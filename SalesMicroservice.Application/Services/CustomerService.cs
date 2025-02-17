using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Sales.Contracts;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBus _bus;

        public CustomerService(ICustomerRepository customerRepository , IBus bus)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _bus = bus; 
        }

        public async Task<Guid> CreateCustomerAsync(CreateCustomerCommand createCustomerCommand)
        {
            try
            {
                var newCustomer = Customer.
                    AddNewCustomer(
                    createCustomerCommand.CustomerDetails.FirstName,

                    createCustomerCommand.CustomerDetails.LastName,
                    createCustomerCommand.CustomerDetails.Email,
                    createCustomerCommand.CustomerDetails.PhoneNumber
                    
                    );
                await _customerRepository.SaveCustomerAsync(newCustomer);
                //brokerConfiguration
                await _bus.PubSub.PublishAsync(new NewCustomerEvent
                {
                   
                    Email = createCustomerCommand.CustomerDetails.Email,
                    FullNames = $"{createCustomerCommand.CustomerDetails.FirstName} {createCustomerCommand.CustomerDetails.LastName}"
                });

                return newCustomer.CustomerId;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> DeleteCustomerAsync(DeleteCustomerCommand deleteCustomerCommand)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(deleteCustomerCommand.deleteCustomerDto.CustomerId);

                if (customer == null)
                {
                    throw new Exception("Customer not found");
                }

                // Delete customer using repository
                var isDeleted = await _customerRepository.DeleteCustomerByIdAsync(customer.CustomerId);

                if (!isDeleted)
                {
                    throw new Exception("Failed to delete customer.");
                }

                return customer.CustomerId;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> UpdateCustomerAsync(UpdateCustomerCommand updateCustomerCommand)
        {
            try
            {

                var customer = await _customerRepository.GetCustomerByIdAsync(updateCustomerCommand.Customer.CustomerId);

                if (customer == null)
                {
                    throw new Exception("Customer not found");
                }


                // Update properties
                customer.FirstName = updateCustomerCommand.Customer.FirstName;
                customer.LastName = updateCustomerCommand.Customer.LastName;
                customer.Email = updateCustomerCommand.Customer.Email;
                customer.PhoneNumber = updateCustomerCommand.Customer.PhoneNumber;

                // Save updates
                var isUpdated = await _customerRepository.UpdateCustomerAsync(customer);


                if (!isUpdated)
                {
                    throw new Exception("Failed to update customer.");
                }

                return customer.CustomerId;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
