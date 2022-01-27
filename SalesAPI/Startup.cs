using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesAPI.Mapper;
using SalesAPI.Persistence;
using SalesAPI.Persistence.Data;
using SalesAPI.Persistence.Repositories;
using SalesAPI.Services;
using System;
using System.Threading.Tasks;

namespace SalesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration["ConnectionStrings:SalesDbSQLServer"];
            services.AddDbContext<SalesDbContext>(options =>
            {
                options
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging();
            });

            services.AddSwaggerGen();

            services.AddScoped<StockRepository, StockRepository>();

            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductMapper, ProductMapper>();
            services.AddScoped<IStockMapper, StockMapper>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ProductSeed>();
            services.AddScoped<StockSeed>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductSeed pSeed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Task.Run(async () => await pSeed.Seed()).Wait();
                                
            }

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "School Manager API");
            }
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}