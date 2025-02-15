using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Application.Dtos
{
  public  record CustomerDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
    }

    public record UpdateCustomerDto
    {

        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNO { get; set; } = string.Empty;
    }



    public record CreateCustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNO { get; set; } = string.Empty;
    }



}
