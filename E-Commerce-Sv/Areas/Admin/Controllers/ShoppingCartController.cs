using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Areas.Admin.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _db;
        public ShoppingCartController(IShoppingCartRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartProducts = _db.GetAll().ToList();
            return View(shoppingCartProducts);
        }
    }
}
