using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class CategoryAddAjaxViewModel
    {
        public CategoryAddDto CategoryAddDto { get; set; }

        public string CategoryAddPartial { get; set; }

        public Category Category { get; set; }
    }
}
