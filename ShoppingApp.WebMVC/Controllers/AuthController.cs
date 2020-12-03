using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Core.Utilities.Hashing;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;

namespace ShoppingApp.WebMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly ICartService _cartService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }

        public IActionResult Register()
        {
            return View(new UserRegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var exist = await _userManager.FindByEmailAsync(model.Email);

            if (exist != null)
            {
                ModelState.AddModelError("", "Email Kullanılmaktadır");
                return View(model);
            }


            var user = new User
            {
                UserName = model.Email.ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                NormalizedUserName = model.Email.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                PhoneNumber = "0",
                ImageUrl = "profile.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginDto());
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("notAvailable.", "Hesap Bulunamadı");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var hasCart = await _cartService.AnyAsync(x => x.UserId == user.Id);

                if (!hasCart)
                {
                    var cart = new Cart()
                    {
                        UserId = user.Id,
                    };
                    await _cartService.AddAsync(cart);

                    await _unitOfWork.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(".", "Beklenmedik Hata Oluştu. Tekrar Deneyiniz.");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ViewResult AccessDenied() => View();
    }
}
