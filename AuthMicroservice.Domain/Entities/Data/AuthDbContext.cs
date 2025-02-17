
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Domain.Entities.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

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
