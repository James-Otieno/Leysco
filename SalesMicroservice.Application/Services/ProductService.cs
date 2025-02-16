using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesMicroservice.Application.Services
{
   public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateProductAsync(CreateProductCommand createProductCommand)
        {
            var product = new Product(createProductCommand.ProductDetails.Name, createProductCommand.ProductDetails.Price, createProductCommand.ProductDetails.Stock);
            var success = await _productRepository.SaveProductAsync(product);
            return success ? product.ProductId : Guid.Empty;
        }

        public async Task<bool> DeleteProductAsync(DeleteProductCommand deleteProductCommand)
        {
            return await _productRepository.DeleteProductAsync(deleteProductCommand.ProductId);
        }

        public async Task<bool> UpdateProductAsync(UpdateProductCommand updateProductCommand)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductByIdAsync(updateProductCommand.ProductDetails.ProductId);
                if (existingProduct == null)
                    return false;

                existingProduct.Name = updateProductCommand.ProductDetails.Name;
                existingProduct.Price = updateProductCommand.ProductDetails.Price;
                existingProduct.Stock = updateProductCommand.ProductDetails.Stock;

                return await _productRepository.UpdateProductAsync(existingProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
