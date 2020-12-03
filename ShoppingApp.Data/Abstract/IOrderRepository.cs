using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Core.Data.Abstract;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Data.Abstract
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
    }
}
