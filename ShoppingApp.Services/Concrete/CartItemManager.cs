using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;

namespace ShoppingApp.Services.Concrete
{
    public class CartItemManager : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartItemManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<CartItem> GetAsync(Expression<Func<CartItem, bool>> predicate,params Expression<Func<CartItem, object>>[] includeProperties)
            => await _unitOfWork.CartItem.GetAsync(predicate, includeProperties);

        public async Task<CartItem> GetById(int id)
            => await _unitOfWork.CartItem.GetAsync(x => x.Id == id, null);

        public async Task<IList<CartItem>> GetAllAsync(Expression<Func<CartItem, bool>> predicate = null, params Expression<Func<CartItem, object>>[] includeProperties)
            => await _unitOfWork.CartItem.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(CartItem entity)
            => await _unitOfWork.CartItem.AddAsync(entity);

        public async Task UpdateAsync(CartItem entity)
            => await _unitOfWork.CartItem.UpdateAsync(entity);

        public async Task DeleteAsync(CartItem entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt =DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(CartItem entity)
            => await _unitOfWork.CartItem.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<CartItem, bool>> predicate)
            => await _unitOfWork.CartItem.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<CartItem, bool>> predicate)
            => await _unitOfWork.CartItem.CountAsync(predicate);

        public IQueryable<CartItem> Quarayable()
            => _unitOfWork.CartItem.Quarayable();

        public async Task<Cart> AddToCart(Cart cart,int productId, int quantity)
        {
            var exist = this.Quarayable().FirstOrDefault(x => x.CartId == cart.Id  && x.ProductId == productId);

            if (exist != null)
            {
                exist.Quantity += quantity;

                await this.UpdateAsync(exist);

            }
            else
            {
                var cartItem = new CartItem()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                await this.AddAsync(cartItem);
            }

            await _unitOfWork.SaveChangesAsync();
            var cartInDb = _unitOfWork.Carts.Quarayable()
                .Include(x=> x.CartItems).FirstOrDefault(x=> x.Id == cart.Id);
            return cartInDb;
        }

        public async Task<Cart> RemoveFromCart(Cart cart, int productId)
        {
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

            await this.HardDelete(cartItem);

            await _unitOfWork.SaveChangesAsync();
            var cartInDb = _unitOfWork.Carts.Quarayable()
                .Include(x => x.CartItems).FirstOrDefault(x => x.Id == cart.Id);
            return cartInDb;
        }
    }
}
