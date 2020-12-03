using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IProductRepository Products { get; }

        ICategoryRepository Categories { get; }

        ICurrencyRepository Currencies { get; }

        ICartRepository Carts { get; }

        ICartItemRepository CartItem { get; }

        IFavoriteRepository Favorites { get; }

        IOrderRepository Orders { get; }

        IOrderItemRepository OrderItems { get; }

        Task<int> SaveChangesAsync();

    }
}
