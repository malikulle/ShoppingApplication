using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Entities.Concrete;

namespace ShoppingApp.Entities.Models
{
    public class Order : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        public OrderState OrderState { get; set; }
        public PaymentType PaymentType { get; set; }

        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public string ConversationId { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }

    }

    public enum OrderState
    {
        Waiting = 0,
        Unpaid = 1,
        Completed = 2
    }

    public enum PaymentType
    {
        EFT = 1,
        CreditCart = 0
    }
}
