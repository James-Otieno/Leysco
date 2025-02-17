using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Domain.Entities
{
    public class AlertsContext:DbContext
    {
        public DbSet<Email> Emails { get; set; }
        public AlertsContext(DbContextOptions<AlertsContext> opt) : base(opt)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>()
                    as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect()) dbCreator.Create();
                    if (!dbCreator.HasTables()) dbCreator.CreateTables();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
