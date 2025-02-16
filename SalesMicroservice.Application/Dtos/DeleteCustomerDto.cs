using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Application.Dtos
{
    public record DeleteCustomerDto
    {
        public Guid CustomerId { get; set; }
    }
}
