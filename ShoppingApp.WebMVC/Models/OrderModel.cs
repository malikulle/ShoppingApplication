using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.WebMVC.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "İsim Zorunludur")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim Zorunludur")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Adres Zorunludur")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Şehir Zorunludur")]
        public string City { get; set; }
        [Required(ErrorMessage = "Telefon Zorunludur")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cart Adı Zorunludur")]
        public string CardName { get; set; }
        [Required(ErrorMessage = "Cart Numarası Zorunludur")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Ay Zorunludur")]
        public string ExpirationMonth { get; set; }
        [Required(ErrorMessage = "Yıl Zorunludur")]
        public string ExpirationYear{ get; set; }
        [Required(ErrorMessage = "CVV Zorunludur")]
        public string Cvv{ get; set; }
        public CartViewModel CartModel{ get; set; }
    }
}
