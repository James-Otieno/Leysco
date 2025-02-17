using Alerts.Microservice.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Application.Services
{
    public interface IAlertsService
    {
        Task<bool> SaveEmailAsync(SendEmailCommand emailCommand);
        Task<bool>SendEmailAsync(SendEmailCommand emailCommand);

    }
}
