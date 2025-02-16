using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Queries
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}
