using ShoppingApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{
    public class UserAjaxModel
    {
        public UserAddDto UserAddDto { get; set; }

        public string PartialView{ get; set; }

        public User User { get; set; }
    }
}
