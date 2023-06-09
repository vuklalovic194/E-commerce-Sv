using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db) 
		{
			_db = db;
		}

		public IActionResult Index()
		{
			List<Category> categories = _db.Categories.ToList();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			_db.Categories.Add(category);
			_db.SaveChanges();
			return RedirectToAction("Index");	
		}

		public IActionResult Edit(int? id) 
		{
			if(id == null || id == 0) 
			{
				return NotFound();
			}
			var category = _db.Categories.FirstOrDefault(c => c.Id == id);
			if(category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{
			_db.Categories.Update(category);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			
			var category = _db.Categories.FirstOrDefault(d=>d.Id == id);

			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Delete(Category category) 
		{
			_db.Categories.Remove(category);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
