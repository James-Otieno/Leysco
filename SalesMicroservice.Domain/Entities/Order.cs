using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

  
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // Default stat
        public decimal TotalAmount { get; set; }

        public Order()
        {
                
        }
        public Order(Guid orderId, Guid customerId, List<OrderItem> items)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Items = items;
            TotalAmount = items.Sum(item => item.Price * item.Quantity);
        }

        public static Order CreateNewOrder(Guid customerId, List<OrderItem> items)
        {
            return new Order(Guid.NewGuid(), customerId, items);
        }
    }

}
