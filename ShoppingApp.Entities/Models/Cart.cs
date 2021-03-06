﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Entities.Models
{
    public class Cart :BaseEntity,IEntity
    {
        public int Id { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual IList<CartItem> CartItems { get; set; }
    }
}
