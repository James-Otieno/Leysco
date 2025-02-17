using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Application.Dtos
{
    public class CustomerSearchDto
    {
        public string? FirstName { get; set; }  // Search by Customer Name

        public string? LastName { get; set; }  // Search by Customer Name
        public string? Email { get; set; }  // Search by Email
        public string? PhoneNumber { get; set; }  // Search by Phone

    }
}
