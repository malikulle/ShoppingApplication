using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Utilities.Extensions;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Areas.Admin.Models;
using ShoppingApp.WebMVC.Helpers.File;

namespace ShoppingApp.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly ICurrencyService _currencyService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public ProductsController(IProductService productService, IUnitOfWork unitOfWork, UserManager<User> userManager, ICategoryService categoryService, IWebHostEnvironment env, IMapper mapper, ICurrencyService currencyService)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _categoryService = categoryService;
            _env = env;
            _mapper = mapper;
            _currencyService = currencyService;
        }

        public async Task<IActionResult> Index()
        {

            var products = _productService.Quarayable().ToList();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = _categoryService.Quarayable().ToList();
            ViewBag.Currencies = _currencyService.Quarayable().ToList();

            return PartialView("_ProductAddPartialView", new ProductAddDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductAddDto model)
        {
            ViewBag.Categories = _categoryService.Quarayable().ToList();
            ViewBag.Currencies = _currencyService.Quarayable().ToList();

            if (!ModelState.IsValid)
            {

                var errorModel = JsonSerializer.Serialize(new ProductAddteAjaxModel()
                {
                    ProductAddPartial = await this.RenderViewToStringAsync("_ProductAddPartialView", model)
                });

                return Json(errorModel);
            }

            var product = _mapper.Map<Product>(model.Product);


            product.ImageUrl = FileUtilities.Base64ToImage(model.Product.ImageUrl, _env);
            product.ProductCategories = model.categoryIds.Select(x => new ProductCategory()
            {
                CategoryId = x
            }).ToList();

            await _productService.AddAsync(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

            var successModel = JsonSerializer.Serialize(new ProductAddteAjaxModel()
            {
                ProductAddPartial = await this.RenderViewToStringAsync("_ProductAddPartialView", model),
                Product = product,
                ProductAddDto = model
            }, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(successModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            ViewBag.Categories = _categoryService.Quarayable().ToList();
            ViewBag.Currencies = _currencyService.Quarayable().ToList();

            var product = _productService.Quarayable()
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                ViewBag.ImageUrl = product.ImageUrl;
                product.ImageUrl = null;
                var model = new ProductAddDto()
                {
                    Product = _mapper.Map<ProductModel>(product),
                    SelectedCategories = product.ProductCategories.Select(x => x.Category).ToList(),
                };


                return PartialView("_ProductUpdatePartialView", model);
            }
            return PartialView("_ProductUpdatePartialView", new ProductAddDto());

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductAddDto model)
        {
            ViewBag.Categories = _categoryService.Quarayable().ToList();
            ViewBag.Currencies = _currencyService.Quarayable().ToList();

            var product = _productService.Quarayable()
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == model.Product.Id);
            ViewBag.ImageUrl = product?.ImageUrl;
            if (!ModelState.IsValid)
            {

                var errorModel = JsonSerializer.Serialize(new ProductUpdateAjaxModel()
                {
                    ProductUpdatePartial = await this.RenderViewToStringAsync("_ProductUpdatePartialView", model)
                });

                return Json(errorModel);
            }

           

            if (model.Product != null)
            {
                product.Name = model.Product.Name;
                product.Description = model.Product.Description;
                product.Price = model.Product.Price;
                product.Description = model.Product.Description;
                product.CurrencyId = model.Product.CurrencyId;
                product.IsFeatured = model.Product.IsFeatured;
                product.IsActive = model.Product.IsActive;
                product.ModifiedAt = DateTime.Now;
                product.ProductCategories = model.categoryIds.Select(x => new ProductCategory() { CategoryId = x, ProductId = model.Product.Id }).ToList();

                if (!string.IsNullOrEmpty(model.Product.ImageUrl))
                {
                    string path = _env.WebRootPath + "/images/" + product.ImageUrl;
                    if(System.IO.File.Exists(path))
                        System.IO.File.Delete(path);

                    product.ImageUrl = FileUtilities.Base64ToImage(model.Product.ImageUrl, _env);
                }
                
                await _unitOfWork.SaveChangesAsync();
            }

            var successModel = JsonSerializer.Serialize(new ProductUpdateAjaxModel()
            {
                Product = product,
                ProductDto = model,
                ProductUpdatePartial = await this.RenderViewToStringAsync("_ProductUpdatePartialView",model)

            }, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            

            return Json(successModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _productService.GetById(productId);

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var path = _env.WebRootPath + "/images/" + product.ImageUrl;

                FileUtilities.DeleteFile(path);
            }

            await _productService.HardDelete(product);

            await _unitOfWork.SaveChangesAsync();

            return Json(new {success = true});
        }


    }
}
