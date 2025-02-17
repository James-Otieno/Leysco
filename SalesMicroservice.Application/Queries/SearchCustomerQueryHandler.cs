using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Application.Queries
{
    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;


        public SearchCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<CustomerDto>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomersAsync();

                var filteredCustomers = customers
                    .Where(c => string.IsNullOrEmpty(request.SearchCriteria.FirstName) || c.FirstName.Contains(request.SearchCriteria.FirstName, StringComparison.OrdinalIgnoreCase))
                    .Where(c => string.IsNullOrEmpty(request.SearchCriteria.Email) || c.Email.Contains(request.SearchCriteria.Email, StringComparison.OrdinalIgnoreCase))
                    .Where(c => string.IsNullOrEmpty(request.SearchCriteria.PhoneNumber) || c.PhoneNumber.Contains(request.SearchCriteria.PhoneNumber))
                    .ToList();

                return _mapper.Map<IEnumerable<CustomerDto>>(filteredCustomers);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
