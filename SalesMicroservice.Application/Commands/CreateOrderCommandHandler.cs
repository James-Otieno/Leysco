using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;

namespace SalesMicroservice.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {

        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler( IOrderService  orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.CreateOrderAsync(request);
        }
    }
}
