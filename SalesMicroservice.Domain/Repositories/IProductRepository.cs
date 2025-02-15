using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroservice.Domain.Repositories
{
    public interface  IProductRepository
    {

        Task<bool> SaveProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid productId);
    }
}
