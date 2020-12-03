using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class OrderAjaxViewModel
    {
        public string PartialView { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
