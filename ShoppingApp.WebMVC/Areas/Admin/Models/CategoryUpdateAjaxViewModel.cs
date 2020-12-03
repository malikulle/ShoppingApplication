using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class CategoryUpdateAjaxViewModel
    {
        public CategoryUpdateDto CategoryUpdateDto { get; set; }

        public string CategoryUpdatePartial { get; set; }

        public Category Category { get; set; }
    }
}
