using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new();

        public Customer()
        {
                
        }
        public Customer(string firstName,string lastName, string email, string phoneNumber)
        {
             CustomerId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Customer AddNewCustomer(string firstName,string lastName,string email, string phoneNumber)
        {
                return new Customer(firstName,lastName,email, phoneNumber);
        }
    }
}
