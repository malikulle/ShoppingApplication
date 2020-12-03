using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class UserUpdateAjaxModel
    {
        public UserUpdateDto UserUpdateDto { get; set; }

        public string PartialView { get; set; }

        public User User { get; set; }
    }
}
