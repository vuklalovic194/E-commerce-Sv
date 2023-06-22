using E_Commerce_Sv.Models;
using E_Commerce_Sv.Models.ViewModels;
using E_Commerce_Sv.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Claims;

namespace E_Commerce_Sv.Areas.Customer.Controllers
{
	[Area("customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartVM ShoppingCartVM { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Summary()
		{
			return View();
		}

		public IActionResult Index()
		{
			var claimsIdenty = (ClaimsIdentity)User.Identity;
			var userId = claimsIdenty.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCartRepository.
				GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product")
			};

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				ShoppingCartVM.OrderTotal += cart.Product.Price * cart.Count;
			}

			return View(ShoppingCartVM);
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
