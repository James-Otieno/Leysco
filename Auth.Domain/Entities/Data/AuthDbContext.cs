using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Auth.Domain.Entities.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //seeding Roles

            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(

                new Role { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },

                new Role { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }

            );
        }


    }
}
