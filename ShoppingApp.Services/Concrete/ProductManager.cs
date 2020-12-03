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
    public class ProductManager :IProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties)
            => await _unitOfWork.Products.GetAsync(predicate, includeProperties);

        public async Task<Product> GetById(int id)
            => await this.GetAsync(x => x.Id == id,null);

        public async Task<IList<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate = null, params Expression<Func<Product, object>>[] includeProperties)
            => await _unitOfWork.Products.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(Product entity)
            => await _unitOfWork.Products.AddAsync(entity);

        public async Task UpdateAsync(Product entity)
        {
            entity.ModifiedAt = DateTime.Now;
            await _unitOfWork.Products.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Product entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt= DateTime.Now;
            await this.UpdateAsync(entity);
        }
        
        public async Task HardDelete(Product entity)
            => await _unitOfWork.Products.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> predicate)
            => await _unitOfWork.Products.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<Product, bool>> predicate)
            => await _unitOfWork.Products.CountAsync(predicate);

        public IQueryable<Product> Quarayable()
            => _unitOfWork.Products.Quarayable();

        public IList<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            var products = _unitOfWork.Products.Quarayable()
                .Include(x=> x.CreatedUser)
                .Include(x=> x.Currency).AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .Where(x => x.ProductCategories.Any(i => i.Category.Name.ToLower() == category.ToLower()));
            }
            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetCountByCategory(string category)
        {
            var products = _unitOfWork.Products.Quarayable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .Where(x => x.ProductCategories.Any(i => i.Category.Name.ToLower() == category.ToLower()));
            }

            return products.Count();
        }
    }
}
