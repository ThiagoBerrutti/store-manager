using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Identity;
using SalesAPI.Models;

namespace SalesAPI.Persistence
{
    public class SalesDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                    UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                    IdentityUserToken<int>>
    {
        public SalesDbContext()
        {
        }

        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        }
    }
}