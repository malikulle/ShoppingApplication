using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class ProductUpdateAjaxModel
    {
        public ProductAddDto ProductDto { get; set; }

        public string ProductUpdatePartial { get; set; }

        public Product Product { get; set; }
    }
}
