using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Core.Services;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Services.Abstract
{
    public interface IFavoriteService : IService<Favorite>
    {
        Task AddToFavorite(int productId, User user);

        Task RemoveFromFavorite(int productId, User user);
    }
}
