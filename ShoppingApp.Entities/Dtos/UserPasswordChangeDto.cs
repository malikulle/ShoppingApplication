using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şuanki Şifreniz")]
        [Required(ErrorMessage = "Boş Olamaz")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "Boş Olamaz")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DisplayName("Yeni Şifre Tekrar")]
        [Required(ErrorMessage = "Boş Olamaz")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string RepeatPassword { get; set; }
    }
}
