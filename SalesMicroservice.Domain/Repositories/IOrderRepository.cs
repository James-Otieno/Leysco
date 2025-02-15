using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> SaveOrderAsync(Order order);
        Task<Product> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Guid OrderId);
    }

}

