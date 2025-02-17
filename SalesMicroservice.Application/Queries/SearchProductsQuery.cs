using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Dtos;

namespace SalesMicroservice.Application.Queries
{
    public class SearchProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public ProductSearchDto SearchCriteria { get; set; }

        public SearchProductsQuery(ProductSearchDto searchCriteria)
        {
            SearchCriteria = searchCriteria;
        }

    }
}
