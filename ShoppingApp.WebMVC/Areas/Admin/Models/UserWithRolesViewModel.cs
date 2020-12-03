using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.Areas.Admin.Models
{

    public class UserWithRolesViewModel
    {
        public User User { get; set; }

        public IList<string> Roles { get; set; }
    }

}
