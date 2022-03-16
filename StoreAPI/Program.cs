using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreAPI.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace StoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                if (!env.IsProduction())
                {
                    var db = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
                    db.Database.Migrate();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}