using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts
{
   public record NewCustomerEvent
    {
         public string FullNames { get; set; }
        public string Email { get; set; }
        

    }
}
