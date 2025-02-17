using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertsMicroservice.Domain.Entities;

namespace AlertsMicroservice.Domain.Repositories
{
   public interface  IAlertsRepository
    {

        Task<bool> SaveAlertAsync(Email email);
    }
}
