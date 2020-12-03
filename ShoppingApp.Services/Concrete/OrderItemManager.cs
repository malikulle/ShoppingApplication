using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;

namespace ShoppingApp.Services.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderItem> GetAsync(Expression<Func<OrderItem, bool>> predicate, params Expression<Func<OrderItem, object>>[] includeProperties)
            => await _unitOfWork.OrderItems.GetAsync(predicate, includeProperties);

        public async Task<OrderItem> GetById(int id)
            => await this.GetAsync(x => x.Id == id);

        public async Task<IList<OrderItem>> GetAllAsync(Expression<Func<OrderItem, bool>> predicate = null, params Expression<Func<OrderItem, object>>[] includeProperties)
            => await _unitOfWork.OrderItems.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(OrderItem entity)
            => await _unitOfWork.OrderItems.AddAsync(entity);

        public async Task UpdateAsync(OrderItem entity)
            => await _unitOfWork.OrderItems.UpdateAsync(entity);

        public async Task DeleteAsync(OrderItem entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(OrderItem entity)
            => await _unitOfWork.OrderItems.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<OrderItem, bool>> predicate)
            => await _unitOfWork.OrderItems.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<OrderItem, bool>> predicate)
            => await _unitOfWork.OrderItems.CountAsync(predicate);

        public IQueryable<OrderItem> Quarayable()
            => _unitOfWork.OrderItems.Quarayable();
    }
}
