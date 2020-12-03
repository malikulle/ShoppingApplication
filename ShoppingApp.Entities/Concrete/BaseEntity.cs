using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.Entities.Concrete
{
    public abstract class BaseEntity
    {
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual DateTime ModifiedAt { get; set; } = DateTime.Now;

        public virtual bool IsActive { get; set; } = true;

        public virtual bool IsDeleted { get; set; } = false;

        public virtual User CreatedUser { get; set; }
        public virtual int? CreatedUserId { get; set; }

    }
}
