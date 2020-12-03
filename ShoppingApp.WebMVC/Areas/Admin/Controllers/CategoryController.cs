using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Core.Utilities.Extensions;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Areas.Admin.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = _categoryService.Quarayable().ToList();

            //var mapperCategory = new CategoryAddDto()
            //{
            //    Name = "Test",
            //    IsActive = true,
            //    IsDeleted = false
            //};

            //var category = _mapper.Map<Category>(mapperCategory);

            return View(categories);
        }

        [HttpGet]
        public IActionResult Post() => PartialView("_CategoryAddPartial");

        [HttpPost]
        public async Task<IActionResult> Post(CategoryAddDto categoryAddDto)
        {
            if (!ModelState.IsValid)
            {
                var categoryAjaxErrorModel = JsonSerializer.Serialize(
                    new CategoryAddAjaxViewModel()
                    {
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto),
                    });
                return Json(categoryAjaxErrorModel);
            }

            var category = _mapper.Map<Category>(categoryAddDto);

            await _categoryService.AddAsync(category);

            await _unitOfWork.SaveChangesAsync();

            var categoryAjaxModel = JsonSerializer.Serialize(
                new CategoryAddAjaxViewModel()
                {
                    CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial",categoryAddDto),
                    Category = category
                });
            return Json(categoryAjaxModel);
        }

        [HttpGet]
        public async Task<IActionResult> Put(int categoryId)
        {

            var category = await _categoryService.GetById(categoryId);

            var categoryMapper = _mapper.Map<CategoryUpdateDto>(category);

            return PartialView("_CategoryUpdatePartial", categoryMapper);
        }

        [HttpPost]
        public async Task<IActionResult> Put(CategoryUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                var errorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
                {
                    CategoryUpdatePartial = await this.RenderViewToStringAsync("_categoryUpdatePartial",model)
                });
                return Json(errorModel);
            }

            var category = await _categoryService.GetById(model.Id);

            category.Name = model.Name;
            category.IsDeleted = model.IsDeleted;
            category.IsActive = model.IsActive;

            await _categoryService.UpdateAsync(category);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var successModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel()
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_categoryUpdatePartial", model),
                Category = category,
                CategoryUpdateDto = model
            });
            return Json(successModel);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var result =  _categoryService.Quarayable().ToList();

            var categories = JsonSerializer.Serialize(result, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await _categoryService.GetById(categoryId);

            await _categoryService.HardDelete(category);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
            }

            return Json(new {success = true});
        }
    }
}
