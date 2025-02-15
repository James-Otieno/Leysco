using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Dtos;

namespace SalesMicroservice.Application.Commands
{
   public class CreateProductCommand :IRequest<Guid>
    {

        public ProductDto Product { get; set; } = new();

    }


    public class UpdateProductCommand : IRequest<bool>
    {
        public ProductDto Product { get; set; } = new();
    }

    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
    }
}
