using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data.Concrete;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Data.Concrete
{
    public class ProductRepository : EfEntityRepositoryBase<Product> ,IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
