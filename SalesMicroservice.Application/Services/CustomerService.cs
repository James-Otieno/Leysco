using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

        }

        public async Task<Guid> CreateCustomerAsync(CreateCustomerCommand customerCommand)
        {
            try
            {
                var newCustomer = new Customer
                {
                    FirstName = customerCommand.Customer.FirstName,
                    LastName = customerCommand.Customer.LastName,
                    Email = customerCommand.Customer.Email,
                    PhoneNumber = customerCommand.Customer.PhoneNumber,
                    
                };

                await _customerRepository.SaveCustomerAsync(newCustomer);
                return newCustomer.CustomerId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Guid> CreateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCustomerAsync(DeleteCustomerCommand customerCommand)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(DeleteCustomerCommand.CustomerId);
                if (customer == null)
                {
                    return false;
                }

                await _customerRepository.DeleteCustomerAsync(customer);
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task DeleteCustomerAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
