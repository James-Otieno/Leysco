using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }


        public Product()
        {
                
        }
        public Product(string name, decimal price, int stock)
        {
                ProductId = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
        }

        public static Product AddNewProduct(string name, decimal price, int stock)
        {
            return new Product (name, price, stock);
        }
    }
}
