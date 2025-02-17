using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesMicroservice.Application.Queries
{
    public class SearchOrdersQueryHandler : IRequestHandler<SearchOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly SalesContext _salescontext;
        private readonly IMapper _mapper;

        public SearchOrdersQueryHandler(SalesContext salescontext, IMapper mapper)
        {

            _mapper = mapper;
            _salescontext = salescontext;

        }

        public async Task<IEnumerable<OrderDto>> Handle(SearchOrdersQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var ordersQuery = _salescontext.Orders.Include(o => o.Customer).AsQueryable();

                //  Search by Order ID
                if (query.SearchCriteria.OrderId.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.OrderId == query.SearchCriteria.OrderId);
                }

                // Search by Customer ID
                if (query.SearchCriteria.CustomerId.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.CustomerId == query.SearchCriteria.CustomerId);
                }

                //  Search by Order Status
                if (query.SearchCriteria.Status.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.Status == query.SearchCriteria.Status);
                }

                //  Search by Date Range
                if (query.SearchCriteria.StartDate.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.OrderDate >= query.SearchCriteria.StartDate.Value);
                }
                if (query.SearchCriteria.EndDate.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.OrderDate<= query.SearchCriteria.EndDate.Value);
                }

                //  Search by Customer First Name (Partial Match)
                if (!string.IsNullOrEmpty(query.SearchCriteria.FirstName))
                {
                    ordersQuery = ordersQuery.Where(o => o.Customer.FirstName.Contains(query.SearchCriteria.FirstName));
                }

                // Search by Customer Last Name (Partial Match)
                if (!string.IsNullOrEmpty(query.SearchCriteria.LastName))
                {
                    ordersQuery = ordersQuery.Where(o => o.Customer.LastName.Contains(query.SearchCriteria.LastName));
                }

                //  Search by Customer Email
                if (!string.IsNullOrEmpty(query.SearchCriteria.Email))
                {
                    ordersQuery = ordersQuery.Where(o => o.Customer.Email == query.SearchCriteria.Email);
                }

                //  Search by Customer Phone Number
                if (!string.IsNullOrEmpty(query.SearchCriteria.PhoneNumber))
                {
                    ordersQuery = ordersQuery.Where(o => o.Customer.PhoneNumber == query.SearchCriteria.PhoneNumber);
                }

                // Fetch results and map to DTOs
                var orders = await ordersQuery.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<OrderDto>>(orders);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}