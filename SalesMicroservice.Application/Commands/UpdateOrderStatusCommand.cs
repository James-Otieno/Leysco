using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Commands
{
   public class UpdateOrderStatusCommand : IRequest<bool>
    {

        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }


        public UpdateOrderStatusCommand(Guid orderId, OrderStatus newStatus)
        {
            OrderId = orderId;
            NewStatus = newStatus;
        }
    }
}
