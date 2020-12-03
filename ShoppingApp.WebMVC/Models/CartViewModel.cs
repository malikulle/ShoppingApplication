using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Models
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
