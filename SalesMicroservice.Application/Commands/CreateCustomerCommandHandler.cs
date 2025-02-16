using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;

namespace SalesMicroservice.Application.Commands
{
  public  class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {

        private readonly ICustomerService _customerService;

        public CreateCustomerCommandHandler(ICustomerService customerService)
        {

            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService)); 
        }

        public Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {


                return _customerService.CreateCustomerAsync(request);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
