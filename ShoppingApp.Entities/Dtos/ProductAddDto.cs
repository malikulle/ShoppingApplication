using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Entities.Dtos
{
    public class ProductAddDto
    {
        public ProductModel Product { get; set; }

        public List<Category> SelectedCategories { get; set; } = new List<Category>();

        public IEnumerable<int> categoryIds { get; set; }
    }

    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad girilmek Zorundadır")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fiyat girilmek Zorundadır")]
        public decimal? Price { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "Stok girilmek Zorundadır")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Para birimi girilmek Zorundadır")]
        public int CurrencyId { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public bool IsFeatured { get; set; } 

    }

}
