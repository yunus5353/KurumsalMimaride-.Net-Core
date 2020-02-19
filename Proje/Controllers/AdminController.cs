using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(productListViewModel);
        }

        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()

            };


            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _productService.Add(product);
            TempData.Add("message", "Product was succesfully added");
            return RedirectToAction("Add");
        }

        public ActionResult Update(int productId)
        {
            var product = new ProductUpdateViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()
            };
            return View(product);
        }

        [HttpPost]
        public ActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _productService.Update(product);
            TempData.Add("message", "Product was succesfully updated");
            return RedirectToAction("Update");
        }

        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            return RedirectToAction("Index");
        }
    }
}
