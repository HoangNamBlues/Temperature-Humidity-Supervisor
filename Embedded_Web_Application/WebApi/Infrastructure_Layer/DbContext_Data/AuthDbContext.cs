using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Core_Layer.Identity;

namespace WebApi.Infrastructure_Layer.DbContext_Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        /* Seed roles ("user" and "admin") for AuthDatabase */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var userRoleId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();
            var roles = new List<ApplicationRole> {
                new ApplicationRole { Id = userRoleId, ConcurrencyStamp = userRoleId.ToString(), Name = "User", NormalizedName = "User".ToUpper()},
                new ApplicationRole { Id = adminRoleId, ConcurrencyStamp = adminRoleId.ToString(), Name= "Admin", NormalizedName = "Admin".ToUpper()}
             };
            modelBuilder.Entity<ApplicationRole>().HasData(roles);
        }
    }
}