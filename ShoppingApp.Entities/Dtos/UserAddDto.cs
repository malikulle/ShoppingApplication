using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Entities.Dtos
{
    public class UserAddDto
    {

        [DisplayName("İsminiz")]
        [Required(ErrorMessage = "Boş Olamaz")]
        public string FirstName { get; set; }

        [DisplayName("Soyisminiz")]
        [Required(ErrorMessage = "Boş Olamaz")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Boş Olamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "Boş Olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "Lütfen Seçiniz")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }

        public string ImageUrl { get; set; }
    }

}
