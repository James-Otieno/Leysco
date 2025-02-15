using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Dtos;

namespace SalesMicroservice.Application.Commands
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public CustomerDto Customer { get; set; } = new();

    }

    public class UpdateCustomerCommand : IRequest<bool>
    {
        public UpdateCustomerDto Customer { get; set; } = new();
    }

    public class DeleteCustomerCommand : IRequest<bool>
    {

        public DeleteCustomerDto Customer { get; set; } = new();
    }

}
