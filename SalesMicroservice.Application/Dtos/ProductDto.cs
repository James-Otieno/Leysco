using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Application.Dtos
{
    public record  ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public record UpdateProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }


    public record CreateProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }


}
