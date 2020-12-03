using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Utilities.Extensions;
using ShoppingApp.Core.Utilities.Hashing;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Areas.Admin.Models;
using ShoppingApp.WebMVC.Helpers.File;

namespace ShoppingApp.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Post() => PartialView("_UserAddPartial");

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(UserAddDto model)
        {
            if (!ModelState.IsValid)
            {
                var errorAjaxModel = JsonSerializer.Serialize(new UserAjaxModel()
                {
                    PartialView = await this.RenderViewToStringAsync("_UserAddPartial", model)
                });
                return Json(errorAjaxModel);
            }

            var exist = await _userManager.FindByEmailAsync(model.Email);


            if (exist != null)
            {
                ModelState.AddModelError("", "Email Bulunmaktadır.");
                var responseAjaxError = JsonSerializer.Serialize(new UserAjaxModel()
                {
                    PartialView = await this.RenderViewToStringAsync("_UserAddPartial", model),
                });
                return Json(responseAjaxError);
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

                var picture = FileUtilities.ImageUpload(model.Email, model.PictureFile, _env);

                user.ImageUrl = picture;
                model.ImageUrl = picture;

                await _userManager.UpdateAsync(user);

                var ajaxModel = JsonSerializer.Serialize(new UserAjaxModel()
                {
                    PartialView = await this.RenderViewToStringAsync("_UserAddPartial", model),
                    UserAddDto = model,
                    User = user
                });
                return Json(ajaxModel);
            }

            var responseAjax = JsonSerializer.Serialize(new UserAjaxModel()
            {
                PartialView = await this.RenderViewToStringAsync("_UserAddPartial", model),
            });
            return Json(responseAjax);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return Json(new { success = false });
            }

            string picture = user.ImageUrl;

            await _userManager.DeleteAsync(user);

            if (picture != "profile.png")
            {
                string path = _env.WebRootPath + "/profile/" + picture;

                FileUtilities.DeleteFile(path);
            }

            return Json(new { success = true });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return Json(new { success = false });
            }

            var model = _mapper.Map<UserUpdateDto>(user);

            return PartialView("_UserUpdatePartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                var errorAjaxModel = JsonSerializer.Serialize(new UserUpdateAjaxModel()
                {
                    PartialView = await this.RenderViewToStringAsync("_UserUpdatePartial", model),
                    UserUpdateDto = model
                });
                return Json(errorAjaxModel);
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.Id);

            if (user == null)
            {
                var errorAjaxModel = JsonSerializer.Serialize(new UserUpdateAjaxModel()
                {
                    PartialView = await this.RenderViewToStringAsync("_UserUpdatePartial", model),
                    UserUpdateDto = model
                });
                return Json(errorAjaxModel);
            }

            if (user.Email != model.Email)
            {
                var exist = await _userManager.FindByEmailAsync(model.Email);

                if (exist != null)
                {
                    ModelState.AddModelError("", "Email Kullanılmaktadır");
                    var errorAjaxModel = JsonSerializer.Serialize(new UserUpdateAjaxModel()
                    {
                        PartialView = await this.RenderViewToStringAsync("_UserUpdatePartial", model),
                        UserUpdateDto = model
                    });
                    return Json(errorAjaxModel);
                }
                else
                {
                    user.Email = model.Email;
                }
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            string picture = user.ImageUrl;

            if (model.PictureFile != null)
            {
                user.ImageUrl = FileUtilities.ImageUpload(model.Email, model.PictureFile, _env);

                if (picture != "profile.png")
                {
                    string path = _env.WebRootPath + "/profile/" + picture;

                    FileUtilities.DeleteFile(path);
                }
            }

            await _userManager.UpdateAsync(user);

            var ajaxModel = JsonSerializer.Serialize(new UserUpdateAjaxModel()
            {
                PartialView = await this.RenderViewToStringAsync("_UserUpdatePartial", model),
                User = user,
                UserUpdateDto = model
            });
            return Json(ajaxModel);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var updateUser = _mapper.Map<UserUpdateDto>(user);

            return View(updateUser);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserPasswordChangeDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var isVerified = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);

            if (!isVerified)
            {
                ModelState.AddModelError("","Şifrenizi Yanlış girdiniz");
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user, model.NewPassword, true, false);
                TempData.Add("SuccessMessage", "Şifreniz Başarılı Bir Şekilde Değiştirilmiştir.");
                return View();
            }
            else
            {
                ModelState.AddModelError("","Şifreyi tekrar giriniz.");
                return View(model);
            }
        }
    }
}
