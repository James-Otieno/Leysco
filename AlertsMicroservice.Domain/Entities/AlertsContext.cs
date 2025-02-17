using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AlertsMicroservice.Domain.Entities
{
   public class AlertsContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }


        public AlertsContext(DbContextOptions<AlertsContext> options) : base(options)
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
