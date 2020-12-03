using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Entities.Models
{
    public class CartItem : BaseEntity,IEntity
    {
        public int Id { get; set; }

        public virtual Cart Cart { get; set; }
        public int CartId { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
