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
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNO { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new();

        public Customer()
        {
                
        }
        public Customer(string name, string email, string phoneNO)
        {
             CustomerId = Guid.NewGuid();
            Name = name;
            Email = email;
            PhoneNO = phoneNO;
        }

        public static Customer AddNewCustomer(string name, string email, string phoneNO)
        {
                return new Customer(name, email, phoneNO);
        }
    }
}
