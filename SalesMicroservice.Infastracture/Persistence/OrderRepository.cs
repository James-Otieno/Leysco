using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Infastracture.Persistence
{
    public class OrderRepository : IOrderRepository
    {

        private readonly SalesContext _salesContext;

        public OrderRepository(SalesContext salesContext)
        {
            _salesContext = salesContext?? throw new ArgumentNullException(nameof(salesContext));
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _salesContext.Orders.FindAsync(orderId);
            if (order == null) return false;

            _salesContext.Orders.Remove(order);
            return await _salesContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _salesContext.Orders.Include(o => o.Items).ToListAsync();
        }

        public async  Task<Order?> GetOrderByIdAsync(Guid orderId)
        {

            return await _salesContext.Orders
               .Include(o => o.Items) 
               .FirstOrDefaultAsync(o => o.OrderId == orderId);

        }

        public async Task<bool> SaveOrderAsync(Order order)
        {
            await _salesContext.Orders.AddAsync(order);
            return await _salesContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _salesContext.Orders.Update(order);
            return await _salesContext.SaveChangesAsync() > 0;
        }
    }
}
