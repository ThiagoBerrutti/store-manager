using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreAPI.Persistence;
using System;
using System.Linq;

namespace Store.API.IntegrationTests
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class//, IDisposable
    {
        public object Lock { get; set; } = new object();
        public object DbLock { get; set; } = new object();
        private static bool _databaseInitialized;

        public string DatabaseName { get; }

        public TestWebApplicationFactory()
        {
            DatabaseName = "TestDb-" + Guid.NewGuid().ToString();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(ConfigureServices);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(sd => sd.ServiceType == typeof(DbContextOptions<StoreDbContext>));
            services.Remove(descriptor);

            var sp = services.BuildServiceProvider();

            services.AddScoped<StoreDbContext>(s => CreateContext());

            EnsureDeleteAndCreateDatabase();
        }

        public StoreDbContext CreateContext()
        {
            var ConnectionString = $"Data Source=localhost,1401;Initial Catalog={DatabaseName};Password=1q2w3e4r@#$;User ID=sa;MultipleActiveResultSets=true";

            return new StoreDbContext(
                new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlServer(ConnectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine)
                .Options);
        }

        public void EnsureDeleteAndCreateDatabase()
        {
            //lock (DbLock)
            {
                using var context = CreateContext();
                _databaseInitialized = false;
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                _databaseInitialized = true;
            }
        }

        public void Cleanup()
        {
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        protected override void Dispose(bool disposing)
        {
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            base.Dispose(disposing);
        }
    }
}