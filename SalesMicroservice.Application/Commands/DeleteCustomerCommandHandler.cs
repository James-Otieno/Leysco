using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;

namespace SalesMicroservice.Application.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Guid>
    {
        private readonly ICustomerService _customerService;

        public DeleteCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        public Task<Guid> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return _customerService.DeleteCustomerAsync(request);
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}

