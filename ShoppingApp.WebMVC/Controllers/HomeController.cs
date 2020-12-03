using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Models;

namespace ShoppingApp.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string category, int page = 1)
        {
            const int pageSize = 6;

            var products = _productService.GetProductsByCategory(category, page, pageSize);
            var totalItems = _productService.GetCountByCategory(category);
            var model = new ProductListModel()
            {
                Products = products,
                PageInfo = new PageInfo()
                {
                    TotalItems = totalItems,
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    CurrentCategory = category
                }
            };
            return View(model);
        }
    }
}
