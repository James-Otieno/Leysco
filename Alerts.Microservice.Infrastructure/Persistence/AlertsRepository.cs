using Alerts.Microservice.Domain.Entities;
using Alerts.Microservice.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Infrastructure.Persistence
{
    public class AlertsRepository : IAlertsRepository
    {
        private readonly AlertsContext _alertsContext;
        public AlertsRepository(AlertsContext alertsContext)
        {
            _alertsContext = alertsContext ?? throw new ArgumentNullException(nameof(alertsContext));
        }
        public async Task<bool> SaveAlertAsync(Email email)
        {
            try
            {
                await _alertsContext.Emails.AddAsync(email);
                await _alertsContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
