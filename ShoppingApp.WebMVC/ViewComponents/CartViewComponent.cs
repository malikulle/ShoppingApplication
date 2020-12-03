using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Models;

namespace ShoppingApp.WebMVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {

        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;

        public CartViewComponent(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var cart =  Cart().Result;

            var model = new CartCountModel()
            {
                CardItemCount = cart.CartItems.Count
            };

            return View(model);
        }

        private async Task<Cart> Cart()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = _cartService.GetCart(user);
            return cart;
        }
    }
}
