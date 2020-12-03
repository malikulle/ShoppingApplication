using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Data.Context.EntityFramework;

namespace ShoppingApp.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppContext _context;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly CurrencyRepository _currencyRepository;
        private readonly CartRepository _cartRepository;
        private readonly CartItemRepository _cartItemRepository;
        private readonly FavoriteRepository _favoriteRepository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        public UnitOfWork(ShoppContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_context);

        public ICurrencyRepository Currencies => _currencyRepository ?? new CurrencyRepository(_context);

        public ICartRepository Carts => _cartRepository ?? new CartRepository(_context);

        public ICartItemRepository CartItem => _cartItemRepository ?? new CartItemRepository(_context);

        public IFavoriteRepository Favorites => _favoriteRepository ?? new FavoriteRepository(_context);
        public IOrderRepository Orders => _orderRepository ?? new OrderRepository(_context);
        public IOrderItemRepository OrderItems => _orderItemRepository ?? new OrderItemRepository(_context);


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
