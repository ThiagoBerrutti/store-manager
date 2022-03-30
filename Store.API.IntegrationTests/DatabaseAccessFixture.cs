using Microsoft.EntityFrameworkCore;
using StoreAPI.Persistence;
using System;

namespace Store.API.IntegrationTests
{
    public class DatabaseAccessFixture
    {
        public string ConnectionString = "Data Source=localhost,1401;Initial Catalog=TestStoreApiDb;Password=1q2w3e4r@#$;User ID=sa;MultipleActiveResultSets=true";

        public StoreDbContext CreateContext()
        {
            return new StoreDbContext(
                new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine)
                .Options);
        }

        public DatabaseAccessFixture()
        {
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Cleanup();
        }

        public void Cleanup()
        {
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}