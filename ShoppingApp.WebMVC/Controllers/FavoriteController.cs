using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;

namespace ShoppingApp.WebMVC.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(UserManager<User> userManager, IFavoriteService favoriteService)
        {
            _userManager = userManager;
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var list = await _favoriteService.GetAllAsync(x => x.UserId == user.Id, x => x.Product,x=> x.User);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> AddToFavorite(int productId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _favoriteService.AddToFavorite(productId, user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromFavorite(int productId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _favoriteService.RemoveFromFavorite(productId, user);

            return RedirectToAction("Index");
        }
    }
}
