using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
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

        

        public async Task<Guid> CreateOrderAsync(CreateOrderCommand createOrderCommand)
        {
            decimal totalWithVAT = CalculateTotalAmount(createOrderCommand.Order.Items); // Calculate the total amount with VAT

            // Create the new order object while passing the total amount including VAT
            var newOrder = Order.CreateNewOrder(
                createOrderCommand.Order.CustomerId,

                createOrderCommand.Order.Items.ConvertAll(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }),
                totalWithVAT // Pass the calculated total with VAT to the order creation
            );

            // Save the order asynchronously to the repository
            await _orderRepository.SaveOrderAsync(newOrder);

            // Return the order ID after saving the order
            return newOrder.OrderId;

        }


        private static decimal CalculateTotalAmount(List<OrderItemDTO> items)
        {
            try
            {
                const decimal vatRate = 0.16m; // VAT at 16%
                decimal totalWithVAT = 0;

                foreach (var item in items)
                {
                    decimal itemTotal = item.Price * item.Quantity; 
                    decimal itemVAT = itemTotal * vatRate;         
                    decimal itemTotalWithVAT = itemTotal + itemVAT; 
                    Console.WriteLine($"[DEBUG] Product: {item.ProductName}, Total: {itemTotal}, VAT: {itemVAT}, Total with VAT: {itemTotalWithVAT}");

                    totalWithVAT += itemTotalWithVAT; 
                }

                Console.WriteLine($"[DEBUG] Order Total with VAT: {totalWithVAT}");
                return totalWithVAT; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error calculating total amount: {ex.Message}");
                throw new InvalidOperationException("Error calculating total amount", ex);
            }
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
                order.TotalAmount = CalculateTotalAmount (updateOrderCommand.Order.Items);

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
