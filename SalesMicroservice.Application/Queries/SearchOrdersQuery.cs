using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Dtos;

namespace SalesMicroservice.Application.Queries
{
    public class SearchOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {

        public OrderSearchDto SearchCriteria { get; set; }
        public SearchOrdersQuery(OrderSearchDto searchCriteria) 
        {
            SearchCriteria = searchCriteria;


        }



    }
}
