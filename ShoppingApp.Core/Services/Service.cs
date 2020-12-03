using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Core.Data.Abstract;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Core.Services
{
    //Pasif
    public class Service<T> : IService<T> where T : BaseEntity, IEntity, new()
    {
        private IEntityRepository<T> _repository;

        public Service(IEntityRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeProperties)
            => await _repository.GetAsync(predicate, includeProperties);

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }


        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
            => await _repository.GetAllAsync(predicate, includeProperties);

        public virtual async Task AddAsync(T entity)
            => await _repository.AddAsync(entity);

        public virtual async Task UpdateAsync(T entity)
            => await _repository.UpdateAsync(entity);

        public virtual async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public virtual async Task HardDelete(T entity)
            => await _repository.DeleteAsync(entity);

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            => await _repository.AnyAsync(predicate);

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
            => await _repository.CountAsync(predicate);

        public IQueryable<T> Quarayable()
            => _repository.Quarayable();
    }
}
