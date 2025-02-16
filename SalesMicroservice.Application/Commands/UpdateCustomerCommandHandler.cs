using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;

namespace SalesMicroservice.Application.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {

        private readonly ICustomerService _customerService;

        public UpdateCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));

        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.UpdateCustomerAsync(request);
            return result != Guid.Empty;
        }
    }
}
