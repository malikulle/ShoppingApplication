using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Data.Abstract;
using ShoppingApp.Entities.Models;
using ShoppingApp.Services.Abstract;
using ShoppingApp.WebMVC.Models;

namespace ShoppingApp.WebMVC.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemService _cartItemService;
        private readonly IOrderService _orderService;

        public CartsController(UserManager<User> userManager, ICartService cartService, ICartItemService cartItemService, IOrderService orderService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _cartService = cartService;
            _cartItemService = cartItemService;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {

            var cart = await Cart();

            var model = new CartViewModel()
            {
                Cart = cart,
                TotalPrice = (decimal) cart.CartItems.Sum(x=> x.Quantity * x.Product.Price)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int productId, int qty = 1)
        {
            var cart = await Cart();

            await _cartItemService.AddToCart(cart, productId, qty);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var cart = await Cart();

            await _cartItemService.RemoveFromCart(cart, productId);

            return Json(new { Success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cart = await Cart();

            var model = new OrderModel()
            {
                CartModel = new CartViewModel()
                {
                    Cart = cart,
                    TotalPrice  = (decimal) cart.CartItems.Sum(x=> x.Product.Price * x.Quantity)
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel model)
        {
            var cart = await Cart();
            var user = await _userManager.GetUserAsync(HttpContext.User);


            model.CartModel = new CartViewModel()
            {
                Cart = cart,
                TotalPrice = (decimal)cart.CartItems.Sum(x => x.Product.Price * x.Quantity)
            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Payment 
            var payment = PaymentProcess(model);
            if (payment.Status.ToUpper() == "SUCCESS")
            {
                await SaveOrder(model, payment, user.Id);
                await ClearCart(user.Id);
                await _unitOfWork.SaveChangesAsync();
                return View("Success");
            }


            return View(model);
        }

        private async Task ClearCart(int userId)
        {
            await _cartService.ClearCartItems(userId);
        }

        private async Task SaveOrder(OrderModel model, Payment payment, int userId)
        {
            var order = new Order()
            {
                OrderNumber = new Random().Next(111111,999999).ToString(),
                OrderState = OrderState.Completed,
                PaymentType = PaymentType.CreditCart,
                PaymentId = payment.PaymentId,
                ConversationId = payment.ConversationId,
                OrderDate = DateTime.Now,
                FirstName = model.FirstName,
                LastName =  model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Address =  model.Address,
                City = model.City,
                UserId = userId,
                OrderItems = model.CartModel.Cart.CartItems.Select(x => new OrderItem
                {
                    Price = (decimal)x.Product.Price,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }).ToList()
            };

            await _orderService.AddAsync(order);
        }

        private async Task<Cart> Cart()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = _cartService.GetCart(user);
            return cart;
        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-fWpwcVa0tG93cq6igdWpm1kz4v0sgdNd";
            options.SecretKey = "sandbox-SVlpc8AbaMdgUNxFyCMX4M2cImZut8KH";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.CartModel.TotalPrice.ToString().Split(",")[0];
            request.PaidPrice = model.CartModel.TotalPrice.ToString().Split(",")[0];
            request.Currency = "TRY";
            request.Installment = 1;
            request.BasketId = model.CartModel.Cart.Id.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;
            //paymentCard.CardHolderName = "John Doe";
            //paymentCard.CardNumber = "5528790000000008";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";
            //paymentCard.RegisterCard = 0;
            //request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;
            foreach (var item in model.CartModel.Cart.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Product.Name;
                basketItem.Category1 = "Phone";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = item.Product.Price.ToString().Split(",")[0];
                basketItems.Add(basketItem);

            }


            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
          
            return payment;
        }
    }
}
