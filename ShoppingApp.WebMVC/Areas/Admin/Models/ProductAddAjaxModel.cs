using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class ProductAddteAjaxModel
    {
        public ProductAddDto ProductAddDto { get; set; }

        public string ProductAddPartial { get; set; }

        public Product Product { get; set; }
    }
}
