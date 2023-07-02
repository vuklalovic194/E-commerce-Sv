using E_Commerce_Sv.Models;
using E_Commerce_Sv.Models.ViewModels;
using E_Commerce_Sv.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace E_Commerce_Sv.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(productList);
        }

        [Authorize]
        public IActionResult Love(Wishlist wishlist)
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			wishlist.ApplicationUserId = userId;


            WishlistVM wishlistVM = new()
            {
                WishList = _unitOfWork.WishlistRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product").ToList(),
                Products = new Product()
            };

			return View(wishlistVM);
        }


        //WISHLIST POST
        [Authorize]
        public IActionResult LovePOST(Wishlist wishlist, int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            wishlist.ApplicationUserId = userId;

            Wishlist wishlistFromDb = _unitOfWork.WishlistRepository.Get(u => u.ApplicationUserId==userId);
            if(wishlistFromDb != null)
            {
                
				_unitOfWork.WishlistRepository.Update(wishlist);
			}
			else
            {
				_unitOfWork.WishlistRepository.Add(wishlist);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        //Remove from wishlist
        [Authorize]
        public IActionResult RemoveLove(Wishlist wishlist, int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            wishlist.ApplicationUserId=userId;

            var wishlistFromDb = _unitOfWork.WishlistRepository.Get(u=>u.ApplicationUserId==userId);
            if (wishlistFromDb != null)
            {
                _unitOfWork.WishlistRepository.Remove(wishlistFromDb);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        //DETAILS GET
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.ProductRepository.Get(u => u.Id == productId),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }


        //DETAILS POST 
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCartRepository.
                Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                //update
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            }
            else if(shoppingCart.Count < 0)
            {
                shoppingCart.Count = 0;
            }
            else
            {
                //create
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
            }
            _unitOfWork.Save();
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}