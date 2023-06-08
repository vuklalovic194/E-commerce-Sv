using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
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
