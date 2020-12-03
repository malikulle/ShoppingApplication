using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Core.Services;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Services.Abstract
{
    public interface IProductService : IService<Product>
    {
        IList<Product> GetProductsByCategory(string category, int page, int pageSize);

        int GetCountByCategory(string category);
    }
}
