using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }

        public bool IsFavorite { get; set; }
    }
}
