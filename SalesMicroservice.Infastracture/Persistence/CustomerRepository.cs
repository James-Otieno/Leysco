using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Infastracture.Persistence
{
    public class CustomerRepository : ICustomerRepository

    {

        private readonly SalesContext _salesContext;
        public CustomerRepository(SalesContext salesContext)
        {
                _salesContext = salesContext??throw new ArgumentNullException(nameof(salesContext));
        }

        public async Task<bool> DeleteCustomerByIdAsync(Guid customerId)
        {
            try
            {
                var customer = await _salesContext.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return false;
                }

                _salesContext.Customers.Remove(customer);
                return await _salesContext.SaveChangesAsync() > 0;


            }
            catch (Exception)
            {

                throw;
            }
        }

       
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _salesContext.Customers.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _salesContext.Customers.FindAsync(customerId);
        }



        public async Task<bool> SaveCustomerAsync(Customer customer)
        {
            await _salesContext.Customers.AddAsync(customer);
            await _salesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _salesContext.Customers.Update(customer);
            return await _salesContext.SaveChangesAsync() > 0;
        }
    }
}
