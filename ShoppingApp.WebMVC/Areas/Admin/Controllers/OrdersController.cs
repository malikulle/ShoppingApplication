using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Utilities.Extensions;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Areas.Admin.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public OrdersController(IOrderService orderService, UserManager<User> userManager, RoleManager<Role> roleManager, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _roleManager = roleManager;
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var roles = await _userManager.GetRolesAsync(user);

            var model = new OrderViewModel();

            if (roles.Any(x => x == "Admin"))
            {
                var orders = _orderService.Quarayable()
                    .Include(x => x.OrderItems)
                    .OrderByDescending(x => x.Id).ToList();

                model.Orders = orders;
            }
            else
            {
                var orders = _orderService.Quarayable()
                    .Include(x => x.OrderItems)
                        .ThenInclude(x => x.Product)
                    .Where(x => x.UserId == user.Id)
                    .OrderByDescending(x => x.Id).ToList();

                model.Orders = orders;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems(int OrderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);


            var orderItems = _orderItemService.Quarayable()
                .Include(x => x.Product)
                .Where(x => x.OrderId == OrderId)
                .ToList();

            var orderViewModel = new OrderAjaxViewModel()
            {
                OrderItems = orderItems,
                PartialView = this.RenderViewToStringAsync("_OrderItemPartial",orderItems).Result
            };

            return Json(orderViewModel);
        }
    }
}
