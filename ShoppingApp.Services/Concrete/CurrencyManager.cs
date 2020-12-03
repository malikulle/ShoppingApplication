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
    public class CurrencyManager : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Currency> GetAsync(Expression<Func<Currency, bool>> predicate,params Expression<Func<Currency, object>>[] includeProperties)
            => await _unitOfWork.Currencies.GetAsync(predicate, includeProperties);

        public async Task<Currency> GetById(int id)
            => await _unitOfWork.Currencies.GetAsync(x => x.Id == id, null);

        public async Task<IList<Currency>> GetAllAsync(Expression<Func<Currency, bool>> predicate = null, params Expression<Func<Currency, object>>[] includeProperties)
            => await _unitOfWork.Currencies.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(Currency entity)
            => await _unitOfWork.Currencies.AddAsync(entity);

        public async Task UpdateAsync(Currency entity)
        {
            entity.ModifiedAt = DateTime.Now;
            await _unitOfWork.Currencies.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Currency entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(Currency entity)
            => await _unitOfWork.Currencies.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<Currency, bool>> predicate)
            => await _unitOfWork.Currencies.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<Currency, bool>> predicate)
            => await _unitOfWork.Currencies.CountAsync(predicate);

        public IQueryable<Currency> Quarayable()
            => _unitOfWork.Currencies.Quarayable();
    }
}
