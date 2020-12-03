using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Models;

namespace ShoppingApp.WebMVC.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.Quarayable().Where(x => x.IsActive && !x.IsDeleted)
                .Include(x => x.ProductCategories)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.ProductCategories.Count(i => i.CategoryId == x.Id)
                }).ToList();

            return View(categories);
        }
    }
}
