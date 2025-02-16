using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Domain.Repositories
{
    public interface ICustomerRepository
    {

        Task<bool> SaveCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerByIdAsync(Guid customerId);


        

    }
}
