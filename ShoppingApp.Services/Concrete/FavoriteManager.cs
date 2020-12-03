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
    public class FavoriteManager : IFavoriteService
    {

        private readonly IUnitOfWork _unitOfWork;

        public FavoriteManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Favorite> GetAsync(Expression<Func<Favorite, bool>> predicate, params Expression<Func<Favorite, object>>[] includeProperties)
            => await _unitOfWork.Favorites.GetAsync(predicate, includeProperties);

        public async Task<Favorite> GetById(int id)
            => await _unitOfWork.Favorites.GetAsync(x => x.Id == id);

        public async Task<IList<Favorite>> GetAllAsync(Expression<Func<Favorite, bool>> predicate = null, params Expression<Func<Favorite, object>>[] includeProperties)
            => await _unitOfWork.Favorites.GetAllAsync(predicate, includeProperties);

        public async Task AddAsync(Favorite entity)
            => await _unitOfWork.Favorites.AddAsync(entity);

        public async Task UpdateAsync(Favorite entity)
            => await _unitOfWork.Favorites.UpdateAsync(entity);

        public async Task DeleteAsync(Favorite entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            await this.UpdateAsync(entity);
        }

        public async Task HardDelete(Favorite entity)
            => await _unitOfWork.Favorites.DeleteAsync(entity);

        public async Task<bool> AnyAsync(Expression<Func<Favorite, bool>> predicate)
            => await _unitOfWork.Favorites.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<Favorite, bool>> predicate)
            => await _unitOfWork.Favorites.CountAsync(predicate);

        public IQueryable<Favorite> Quarayable()
            =>  _unitOfWork.Favorites.Quarayable();

        public async Task AddToFavorite(int productId, User user)
        {
            var entity = new Favorite()
            {
                ProductId = productId,
                UserId = user.Id
            };

            await this.AddAsync(entity);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveFromFavorite(int productId, User user)
        {
            var favorite = await this.GetAsync(x => x.ProductId == productId && x.UserId == user.Id,null);

            await this.HardDelete(favorite);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
