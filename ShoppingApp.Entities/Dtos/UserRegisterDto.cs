using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Entities.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Lütfen Email Giriniz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli Bir Email Adresi Tanımlayınız")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Lütfen Parolanızı Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen Onay Parolanızı Giriniz")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
