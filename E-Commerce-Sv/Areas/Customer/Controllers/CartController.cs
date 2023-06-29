using E_Commerce_Sv.Models;
using E_Commerce_Sv.Models.ViewModels;
using E_Commerce_Sv.Repository.IRepository;
using E_Commerce_Sv.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Drawing.Text;
using System.Security.Claims;

namespace E_Commerce_Sv.Areas.Customer.Controllers
{
	[Area("customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public ShoppingCartVM ShoppingCartVM { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


        public IActionResult Index()
        {
            var claimsIdenty = (ClaimsIdentity)User.Identity;
            var userId = claimsIdenty.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartRepository.
                GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Product.Price * cart.Count;
            }

            return View(ShoppingCartVM);
        }

		//summary get
        public IActionResult Summary()
		{
            var claimsIdenty = (ClaimsIdentity)User.Identity;
            var userId = claimsIdenty.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetAll
				(u => u.ApplicationUserId == userId, includeProperties: "Product"),
				OrderHeader = new()
			};
			
			ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId);

			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.Phone = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
			
			foreach(var cart in ShoppingCartVM.ShoppingCartList)
			{
				ShoppingCartVM.OrderHeader.OrderTotal += cart.Product.Price * cart.Count;
			}

            return View(ShoppingCartVM);
		}

		//summary post
		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPOST()
		{
			//creating id for appUser
			var claimsIdenty = (ClaimsIdentity)User.Identity;
			var userId = claimsIdenty.FindFirst(ClaimTypes.NameIdentifier).Value;

			//populating cartList 
			ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetAll
				(u => u.ApplicationUserId == userId, includeProperties: "Product");

			//assigning date to orderdate and userId to appUser
			ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

			//creating new App user
			ApplicationUser applicationUser = _unitOfWork.ApplicationUserRepository
				.Get(u => u.Id == userId);

			//calculating Order Total
			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				ShoppingCartVM.OrderHeader.OrderTotal += cart.Product.Price * cart.Count;
			}

			//regular customer
			ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymenStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;

			//adding and saving order header to DB
			_unitOfWork.OrderHeaderRepository.Add(ShoppingCartVM.OrderHeader);
			_unitOfWork.Save();

			//creating Order Detail
			foreach(var cart in ShoppingCartVM.ShoppingCartList)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Product.Price,
					Count = cart.Count
				};
				_unitOfWork.OrderDetailRepository.Add(orderDetail);
				_unitOfWork.Save();
			};

			//adding stripe logic
			var domain = "https://localhost:7087/";
			var options = new SessionCreateOptions
			{
				SuccessUrl = domain + $"customer/cart/orderconformation?id={ShoppingCartVM.OrderHeader.Id}",
				LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment"
			};

			foreach(var item in ShoppingCartVM.ShoppingCartList)
			{
				var sessionLineItem = new SessionLineItemOptions()
				{
					PriceData = new SessionLineItemPriceDataOptions()
					{
						UnitAmount = item.Product.Price * 100,
						Currency = "usd",
						ProductData = new()
						{
							Name = item.Product.Name
						}
					},
					Quantity = item.Count
				};
				options.LineItems.Add(sessionLineItem);
			}

			//updating session id and payment id
			var service = new SessionService();
			Session session = service.Create(options);
			_unitOfWork.OrderHeaderRepository
				.UpdatePaymentIntentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
			_unitOfWork.Save();

			//payment Id will update after successful payment
			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);

		}

		//SKAPIRAJ STA SE DESAVA TACNO
		public IActionResult OrderConformation(int id)
		{
			OrderHeader orderHeader = _unitOfWork.OrderHeaderRepository.Get(u=>u.Id == id, includeProperties:"ApplicationUser");
			if (orderHeader.PaymentStatus == SD.PaymenStatusPending)
			{
				var service= new SessionService();
				Session session = service.Get(orderHeader.SessionId);

				if (session.PaymentStatus.ToLower() == "paid")
				{
					_unitOfWork.OrderHeaderRepository.UpdatePaymentIntentID(id, session.PaymentIntentId, session.Id);
					_unitOfWork.OrderHeaderRepository.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
					_unitOfWork.Save();
				}

				List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCartRepository.GetAll(u=> u.ApplicationUserId == orderHeader.ApplicationUser.Id).ToList();
				_unitOfWork.ShoppingCartRepository.RemoveRange(shoppingCarts);
				_unitOfWork.Save();
			}
			return View(id);
		}



		public IActionResult Plus(int cartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.Product.Id == cartId);
			cartFromDb.Count += 1;
			_unitOfWork.ShoppingCartRepository.Update(cartFromDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int cartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.Product.Id == cartId);
			if (cartFromDb.Count <= 1)
			{
				_unitOfWork.ShoppingCartRepository.Remove(cartFromDb);
			}
			else
			{
				cartFromDb.Count -= 1;
				_unitOfWork.ShoppingCartRepository.Update(cartFromDb);

			}
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int cartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.ProductId == cartId);
			_unitOfWork.ShoppingCartRepository.Remove(cartFromDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
	}
}
