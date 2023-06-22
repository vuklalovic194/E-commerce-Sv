using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;
using E_Commerce_Sv.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index()
		{
			List<Category> category = _unitOfWork.CategoryRepository.GetAll().ToList();
			return View(category);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.CategoryRepository.Add(category);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id) 
		{
			if(id == null || id == 0) 
			{
				return NotFound();
			}
			Category? categoryFromDb = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
			if(categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Category category) 
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.CategoryRepository.Update(category);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id) 
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _unitOfWork.CategoryRepository.Get(c => c.Id == id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Delete(Category category) 
		{
			_unitOfWork.CategoryRepository.Remove(category);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
