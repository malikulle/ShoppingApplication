using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Entities.Models
{
    public class UserToken : IdentityUserToken<int>
    {
    }
}
