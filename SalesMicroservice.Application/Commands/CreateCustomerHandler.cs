using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Application.Commands
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>   
    {
        public readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

        }
    }
}
