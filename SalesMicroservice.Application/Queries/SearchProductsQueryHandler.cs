using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesMicroservice.Application.Queries
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly SalesContext _salescontext;
        private readonly IMapper _mapper;

        public SearchProductsQueryHandler(SalesContext salesContext, IMapper mapper)
        {
            
            _mapper = mapper;
            _salescontext = salesContext;
        }

        public async Task<IEnumerable<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {

            var query = _salescontext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchCriteria.ProductId))
            {
                query = query.Where(p => p.ProductId.ToString() == request.SearchCriteria.ProductId);
            }
            if (!string.IsNullOrEmpty(request.SearchCriteria.Name))
            {
                query = query.Where(p => p.Name.Contains(request.SearchCriteria.Name));
            }
            if (request.SearchCriteria.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.SearchCriteria.MinPrice.Value);
            }
            if (request.SearchCriteria.MaxPrice.HasValue && request.SearchCriteria.MaxPrice > 0)
            {
                query = query.Where(p => p.Price <= request.SearchCriteria.MaxPrice.Value);
            }
            if (request.SearchCriteria.MinStock.HasValue)
            {
                query = query.Where(p => p.Stock >= request.SearchCriteria.MinStock.Value);
            }
            if (request.SearchCriteria.MaxStock.HasValue && request.SearchCriteria.MaxStock > 0)
            {
                query = query.Where(p => p.Stock <= request.SearchCriteria.MaxStock.Value);
            }

            var result = await query.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(result);


        }
    }
    }

