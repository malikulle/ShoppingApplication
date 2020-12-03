using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Abstract;

namespace ShoppingApp.WebMVC.ViewComponents
{
    public class FeaturedProductListViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public FeaturedProductListViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var products = _productService.Quarayable()
                .Include(x => x.Currency)
                .Where(x => x.IsFeatured).Take(5).ToList();
            return View(products);
        }

    }
}
