using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Application.Dtos
{
    public record OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }

    public record OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public record CreateOrderDTO
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }

    public record UpdateOrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }


    public record DeleteOrderDto
    {
        public Guid OrderId { get; set; }



    }


}
