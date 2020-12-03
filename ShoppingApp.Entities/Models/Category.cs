using System;
using System.Collections.Generic;
using System.Text;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Entities.Models
{
    public class Category : BaseEntity,IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<ProductCategory> ProductCategories { get; set; }
    }
}
