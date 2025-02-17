using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Dtos
{
  public record OrderSearchDto
    {
        public Guid? CustomerId { get; set; }  // Search orders by Customer ID
        public Guid? OrderId { get; set; }  // Search orders by Order ID
        public DateTime? StartDate { get; set; }  // Filter orders by date range
        public DateTime? EndDate { get; set; }
        public OrderStatus? Status { get; set; }  // Search orders by status


        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}

