using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertsMicroservice.Domain.Entities;
using AlertsMicroservice.Domain.Repositories;

namespace AlertsMicroservice.Infastracture.Persistence
{
   public class AlertsRepository : IAlertsRepository
    {
        private readonly AlertsContext _alertsContext;

        public AlertsRepository(AlertsContext alertsContext)
        {

            _alertsContext = alertsContext;

        }

        public async  Task<bool> SaveAlertAsync(Email email)
        {
            try
            {
                await _alertsContext.Emails.AddAsync(email);
                await _alertsContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
