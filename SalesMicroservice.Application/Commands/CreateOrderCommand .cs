using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Dtos;

namespace SalesMicroservice.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {

        public OrderDto Order { get; set; } = new();
    }


    public class CancelOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
    }
}
