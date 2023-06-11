﻿using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Sv.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository db)
        {
            _productRepo = db;
        }

        public IActionResult Index()
        {
            List<Product> products = _productRepo.GetAll().ToList();
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
            _productRepo.Add(product);
            _productRepo.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Read()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _productRepo.Get(x => x.Id == id);
            if (productFromDb == null)
            {
                NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _productRepo.Update(product);
            _productRepo.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _productRepo.Get(x => x.Id == id);
            if (productFromDb == null)
            {
                NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _productRepo.Remove(product);
            _productRepo.Save();
            return RedirectToAction("Index");
        }
    }
}