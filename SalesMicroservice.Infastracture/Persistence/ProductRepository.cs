using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesMicroservice.Domain.Entities;
using SalesMicroservice.Domain.Repositories;

namespace SalesMicroservice.Infastracture.Persistence
{
    public class ProductRepository:IProductRepository
    {
        private readonly SalesContext _salesContext;

        public ProductRepository(SalesContext salesContext)
        {
            _salesContext = salesContext??throw new ArgumentNullException(nameof(salesContext));
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            try
            {
                var product = await _salesContext.Products.FindAsync(productId);
                if (product == null)
                {
                    return false;
                }

                _salesContext.Products.Remove(product);
                return await _salesContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _salesContext.Products.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await GetProductByIdAsync(productId);
        }

        public async Task<bool> SaveProductAsync(Product product)
        {
            await _salesContext.Products.AddAsync(product);
            return await _salesContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _salesContext.Products.Update(product);
            return await _salesContext.SaveChangesAsync() > 0;
        }
    }
}
