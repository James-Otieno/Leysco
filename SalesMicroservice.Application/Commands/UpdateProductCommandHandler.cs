using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;

namespace SalesMicroservice.Application.Commands
{
   public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler( IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductAsync(request);
        }
    }
}
