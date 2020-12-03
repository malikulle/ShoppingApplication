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
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Cart> GetAsync(Expression<Func<Cart, bool>> predicate, params Expression<Func<Cart, object>>[] includeProperties)
            => await _unitOfWork.Carts.GetAsync(predicate, includeProperties);

        public async Task<Cart> GetById(int id)
            => await _unitOfWork.Carts.GetAsync(x => x.Id == id, null);

        public async Task<IList<Cart>> GetAllAsync(Expression<Func<Cart, bool>> predicate = null, params Expression<Func<Cart, object>>[] includeProperties)
            => await _unitOfWork.Carts.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(Cart entity)
            => await _unitOfWork.Carts.AddAsync(entity);

        public async Task UpdateAsync(Cart entity)
            => await _unitOfWork.Carts.UpdateAsync(entity);

        public async Task DeleteAsync(Cart entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(Cart entity)
            => await _unitOfWork.Carts.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<Cart, bool>> predicate)
            => await _unitOfWork.Carts.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<Cart, bool>> predicate)
            => await _unitOfWork.Carts.CountAsync(predicate);

        public IQueryable<Cart> Quarayable()
            => _unitOfWork.Carts.Quarayable();

        public Cart GetCart(User user)
        {
            var cart = this.Quarayable()
                .Include(x => x.CartItems)
                    .ThenInclude(x=> x.Product)
                        .ThenInclude(x=> x.Currency)
                .FirstOrDefault(x => x.UserId == user.Id);


            return cart;
        }

        public async Task ClearCartItems(int UserId)
        {
            var cart = this
                .Quarayable().Include(x => x.CartItems).FirstOrDefault(x => x.UserId == UserId);

            foreach (var item in cart.CartItems)
            {
                await _unitOfWork.CartItem.DeleteAsync(item);
            }
        }
    }
}
