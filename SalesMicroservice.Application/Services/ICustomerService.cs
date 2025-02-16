using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Services
{
   public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(CreateCustomerCommand createCustomerCommand);
        Task<Guid> UpdateCustomerAsync(UpdateCustomerCommand updateCustomerCommand);
        Task<Guid> DeleteCustomerAsync(DeleteCustomerCommand deleteCustomerCommand);

    }
}
