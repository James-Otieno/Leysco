using Alerts.Microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Domain.Repositories
{
    public interface IAlertsRepository
    {
        Task<bool> SaveAlertAsync(Email email);
    }
}
