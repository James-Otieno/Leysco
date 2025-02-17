using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMicroservice.Application.Dtos
{
     public record  ProductSearchDto
    {
        public string? Name { get; set; }  // Search by Name

        public string ? ProductId { get; set; }  // Search by ProductId
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }


    }
}
