using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
		{
			List<Product> products = _db.Products.ToList();
			//var products = _db.Products.ToList();
			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			_db.Products.Add(product);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Read()
		{
			return RedirectToAction("Index");
		}

		public IActionResult Edit()
		{
			return View();
		}

		public IActionResult Delete()
		{
			return View();
		}
	}
}
