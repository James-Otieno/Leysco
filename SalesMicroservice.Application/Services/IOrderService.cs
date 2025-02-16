using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderCommand createOrderCommand);
        Task<bool> UpdateOrderAsync(UpdateOrderCommand updateOrderCommand);
        Task<bool> CancelOrderAsync(CancelOrderCommand cancelOrderCommand);
        Task<OrderDto?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusCommand updateStatusCommand);

    }
}
