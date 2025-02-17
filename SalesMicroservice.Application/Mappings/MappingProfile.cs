using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Product, ProductDto>();

        }
    }
}
