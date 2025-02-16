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

        public ProductDto ProductDetails { get; set; } = new();

    }

    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
    }


    public class UpdateProductCommand : IRequest<bool>
    {
        public UpdateProductDto ProductDetails { get; set; } = new();
    }


}
