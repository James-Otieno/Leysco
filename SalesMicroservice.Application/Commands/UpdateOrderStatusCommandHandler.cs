using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Application.Commands
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (order == null)
            {
                return false; // Order not found
            }

            order.Status = request.NewStatus;
            return await _orderRepository.UpdateOrderAsync(order);
        }
    }
    
}
