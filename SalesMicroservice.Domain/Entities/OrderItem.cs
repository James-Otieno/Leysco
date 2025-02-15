using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderItem()
        {
                
        }
        public OrderItem(Guid orderItemId, Guid orderId, Guid productId, int quantity, decimal price)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public  static OrderItem AddNewOrderItem(Guid orderId, Guid productId, int quantity, decimal price)
        {
            return new OrderItem(Guid.NewGuid(), orderId, productId, quantity, price);
        }
    }
}
