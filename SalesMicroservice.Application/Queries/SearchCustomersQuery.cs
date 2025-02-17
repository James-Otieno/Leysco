using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Dtos;

namespace SalesMicroservice.Application.Queries
{
    public class SearchCustomersQuery: IRequest<IEnumerable<CustomerDto>>
    {
        public CustomerSearchDto SearchCriteria { get; set; }
        public SearchCustomersQuery(CustomerSearchDto searchCriteria)
        {
            SearchCriteria = searchCriteria;
        }
    }
    
}
