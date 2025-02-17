﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SalesMicroservice.Application.Services;

namespace SalesMicroservice.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {

        private readonly IProductService _productService;
        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.CreateProductAsync(request);
        }
    }
}


