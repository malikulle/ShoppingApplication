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
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> predicate, params Expression<Func<Order, object>>[] includeProperties)
            => await _unitOfWork.Orders.GetAsync(predicate, includeProperties);

        public async Task<Order> GetById(int id)
            => await this.GetAsync(x => x.Id == id);

        public async Task<IList<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null, params Expression<Func<Order, object>>[] includeProperties)
            => await _unitOfWork.Orders.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(Order entity)
            => await _unitOfWork.Orders.AddAsync(entity);

        public async Task UpdateAsync(Order entity)
            => await _unitOfWork.Orders.UpdateAsync(entity);

        public async Task DeleteAsync(Order entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(Order entity)
            => await _unitOfWork.Orders.DeleteAsync(entity);



        public async Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate)
            => await _unitOfWork.Orders.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<Order, bool>> predicate)
            => await _unitOfWork.Orders.CountAsync(predicate);

        public IQueryable<Order> Quarayable()
            => _unitOfWork.Orders.Quarayable();
    }
}
