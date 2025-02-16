using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesMicroservice.Application.Services
{
   public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository)); 
                
        }

        public async Task<bool> CancelOrderAsync(CancelOrderCommand cancelOrderCommand)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(cancelOrderCommand.OrderId);
                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                return await _orderRepository.DeleteOrderAsync(order.OrderId);



            }
            catch (Exception)
            {

                throw;
            }
        }

            private decimal CalculateTotalAmount(List<OrderItemDTO> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Price * item.Quantity;
            }
            return total;



        }

        public async Task<Guid> CreateOrderAsync(CreateOrderCommand createOrderCommand)
        {
            var newOrder = Order.CreateNewOrder(
                createOrderCommand.Order.CustomerId,
                createOrderCommand.Order.Items.ConvertAll(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                })
            );

            await _orderRepository.SaveOrderAsync(newOrder);
            return newOrder.OrderId;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(item => new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            });
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(item => new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public async Task<bool> UpdateOrderAsync(UpdateOrderCommand updateOrderCommand)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(updateOrderCommand.Order.OrderId);
                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                // Update existing order instead of creating a new one
                order.CustomerId = updateOrderCommand.Order.CustomerId;
                order.Items = updateOrderCommand.Order.Items.ConvertAll(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
                order.TotalAmount = CalculateTotalAmount(updateOrderCommand.Order.Items);

                return await _orderRepository.UpdateOrderAsync(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async  Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusCommand updateStatusCommand)
        {
            var order = await _orderRepository.GetOrderByIdAsync(updateStatusCommand.OrderId);
            if (order == null) return false;

            order.Status = updateStatusCommand.NewStatus;
            return await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
