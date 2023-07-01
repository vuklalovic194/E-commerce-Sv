using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Models.ViewModels;
using E_Commerce_Sv.Repository.IRepository;
using E_Commerce_Sv.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace E_Commerce_Sv.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class ProductController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
			_unitOfWork = db;
		}

		public IActionResult Index()
		{
			List<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
			
			//var products = _db.Products.ToList();
			return View(products);
		}

		public IActionResult Upsert(int? id)
		{
			ProductVM productVM = new()
			{
				CategoryList = _unitOfWork.CategoryRepository.
				GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = new Product()
			};
			if (id == 0 || id == null)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
				return View(productVM);
			}
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM obj, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
					{
						var oldImagetPath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagetPath))
						{
							System.IO.File.Delete(oldImagetPath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					
					obj.Product.ImageUrl = @"\images\product\" + fileName;
				}
				if (obj.Product.Id == 0)
				{
					_unitOfWork.ProductRepository.Add(obj.Product);
				}
				else
				{
					_unitOfWork.ProductRepository.Update(obj.Product);
				}
				
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			else
			{
				obj.CategoryList = _unitOfWork.CategoryRepository.
				GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(obj);
			}
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Product? productFromDb = _unitOfWork.ProductRepository.Get(x => x.Id == id);
			if (productFromDb == null)
			{
				NotFound();
			}
			return View(productFromDb);
		}

		[HttpPost]
		public IActionResult Delete(Product product)
		{
			_unitOfWork.ProductRepository.Remove(product);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
