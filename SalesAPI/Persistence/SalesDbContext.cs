using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;

namespace SalesAPI.Persistence
{
    public class SalesDbContext : DbContext
    {
        //    private StockSeed stockSeed = new StockSeed();
        //private ProductSeed productSeed = new ProductSeed();
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