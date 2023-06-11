using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
		{
			List<Category> category = _categoryRepo.GetAll().ToList();
			return View(category);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			_categoryRepo.Add(category);
			_categoryRepo.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int? id) 
		{
			if(id == null || id == 0) 
			{
				return NotFound();
			}
			Category? categoryFromDb = _categoryRepo.Get(c => c.Id == id);
			if(categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Category category) 
		{
			_categoryRepo.Update(category);
			_categoryRepo.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int? id) 
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _categoryRepo.Get(c => c.Id==id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Delete(Category category) 
		{
			_categoryRepo.Remove(category);
			_categoryRepo.Save();
			return RedirectToAction("Index");
		}
	}
}
