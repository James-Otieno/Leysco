using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertsMicroservice.Application.Command;

namespace AlertsMicroservice.Application.Services
{
    public interface IAlertsService
    {
        Task<bool> SaveEmailAsync(SendEmailCommand emailCommand);
        Task<bool> SendEmailAsync(SendEmailCommand emailCommand);


    }
}
