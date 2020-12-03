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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> predicate,params Expression<Func<Category, object>>[] includeProperties)
            => await _unitOfWork.Categories.GetAsync(predicate, includeProperties);

        public async Task<Category> GetById(int id)
            => await this.GetAsync(x => x.Id == id, null);

        public async Task<IList<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate = null,
            params Expression<Func<Category, object>>[] includeProperties)
            => await _unitOfWork.Categories.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(Category entity)
            => await _unitOfWork.Categories.AddAsync(entity);

        public async Task UpdateAsync(Category entity)
        {
            entity.ModifiedAt = DateTime.Now;
            await _unitOfWork.Categories.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(Category entity)
            => await _unitOfWork.Categories.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
            => await _unitOfWork.Categories.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<Category, bool>> predicate)
            => await _unitOfWork.Categories.CountAsync(predicate);

        public IQueryable<Category> Quarayable()
            => _unitOfWork.Categories.Quarayable();

        public int ProductCount(int CategoryId)
            => this
                .Quarayable()
                .Include(x => x.ProductCategories)
                .Count(x => x.ProductCategories.Any(i => i.CategoryId == CategoryId));

    }
}
