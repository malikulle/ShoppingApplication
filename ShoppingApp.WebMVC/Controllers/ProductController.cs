using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Models;

namespace ShoppingApp.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteService _favoriteService;

        public ProductController(IProductService productService, UserManager<User> userManager, IFavoriteService favoriteService)
        {
            _productService = productService;
            _userManager = userManager;
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = _productService.Quarayable()
                .Include(x=> x.ProductCategories)
                .ThenInclude(x=>x.Category)
                .Include(x => x.Currency)
                .FirstOrDefault(x => x.Id == id);

            var user = await _userManager.GetUserAsync(HttpContext.User);

            bool favorite = false;

            if (user != null)
            {
                favorite = await _favoriteService.AnyAsync(x => x.UserId == user.Id && x.ProductId == id);

            }



            var model = new ProductDetailModel()
            {
                Product = product,
                IsFavorite = favorite
            };

            return View(model);
        }
    }
}
