using System;
using System.Collections.Generic;
using System.Text;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Entities.Models
{
    public class Product : BaseEntity, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public string Description { get; set; }

        public int? Stock { get; set; }

        public bool IsFeatured { get; set; } = false;

        public virtual Currency Currency { get; set; }
        public int? CurrencyId { get; set; }

        public virtual IList<ProductCategory> ProductCategories { get; set; }
    }
}
