using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Models
{
    public class ProductListModel
    {
        public IList<Product> Products{ get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
