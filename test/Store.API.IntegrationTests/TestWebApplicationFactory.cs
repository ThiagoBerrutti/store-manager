using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoreAPI.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.IntegrationTests
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
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
            var dbContextDescriptor = services.SingleOrDefault(sd => sd.ServiceType == typeof(DbContextOptions<StoreDbContext>));
            var configJsonOptions = services.SingleOrDefault(sd => sd.ServiceType == typeof(IConfigureOptions<JsonOptions>));

            services.Remove(configJsonOptions);
            services.Remove(dbContextDescriptor);

            services.AddScoped<StoreDbContext>(s => CreateContext());

            var sp = services.BuildServiceProvider();
            var context = sp.GetRequiredService<StoreDbContext>();

            context.Database.EnsureCreated();
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

        public async Task Cleanup()
        {
            using var context = CreateContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using var context = CreateContext();
                context.Database.EnsureDeleted();
                base.Dispose(disposing);
            }
        }
    }
}