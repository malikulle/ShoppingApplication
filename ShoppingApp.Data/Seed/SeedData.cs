using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Context.EntityFramework;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Data.Seed
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            using (var context = new ShoppContext())
            {
                if (!context.Products.Any())
                {
                    List<Product> products = new List<Product>()
                    {
                        new Product(){Name = "Samsung S5",Price = 2000 ,ImageUrl = "1.jpg" ,Stock = 2,},
                        new Product(){Name = "Samsung S6",Price = 3000 ,ImageUrl = "2.jpg",Stock = 2},
                        new Product(){Name = "Samsung S7",Price = 4000 ,ImageUrl = "3.jpg",Stock = 2},
                        new Product(){Name = "Samsung S8",Price = 5000 ,ImageUrl = "4.jpg",Stock = 2},
                        new Product(){Name = "Samsung S9",Price = 6000 ,ImageUrl = "5.jpg",Stock = 2},
                        new Product(){Name = "IPhone 6",Price = 4000 ,ImageUrl = "6.jpg",Stock = 2},
                        new Product(){Name = "IPhone 7",Price = 5000 ,ImageUrl = "1.jpg",Stock = 2}
                    };

                    context.Products.AddRange(products);;
                    context.SaveChanges();
                }
            }
        }
    }
}
