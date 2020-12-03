using System;
using System.Collections.Generic;
using System.Text;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Entities.Models
{
    public class ProductCategory : IEntity
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
