using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Application.Services
{
    public interface IProductService
    {
      //  Task<IEnumerable<Product>> GetAllProductsAsync();
       // Task<Product?> GetProductByIdAsync(Guid productId);
        Task<Guid> CreateProductAsync(CreateProductCommand createProductCommand);
        Task<bool> UpdateProductAsync(UpdateProductCommand updateProductCommand);
        Task<bool> DeleteProductAsync(DeleteProductCommand deleteProductCommand);


    }
}
