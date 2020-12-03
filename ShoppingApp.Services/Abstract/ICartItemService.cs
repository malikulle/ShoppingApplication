using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Core.Services;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Services.Abstract
{
    public interface ICartItemService : IService<CartItem>
    {
        Task<Cart> AddToCart(Cart cart, int productId, int quantity);

        Task<Cart> RemoveFromCart(Cart cart, int productId);
    }
}
