using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using StoreAPI.Exceptions;
using StoreAPI.Extensions;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Persistence;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Services;
using StoreAPI.Swagger;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StoreAPI
{
    public class Startup
    {
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var section = Configuration.GetSection("StoreDbSQLServerConnectionStringSettings");
            
            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                WorkstationID = section["WorkstationId"],
                DataSource = section["DataSource"],
                InitialCatalog = section["InitialCatalog"],
                MultipleActiveResultSets = bool.Parse(section["MultipleActiveResultSets"]),
                UserID = section["UserId"],
                Password = section["DbPassword"]
            };

            var connectionString = connectionStringBuilder.ConnectionString;

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

                if (_env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging()
                           .EnableDetailedErrors()
                           .LogTo(Console.WriteLine);
                }
                else
                {
                    options.LogTo(Console.WriteLine, LogLevel.Error);
                }
            });

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = AppConstants.Validations.User.PasswordMinLength;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<StoreDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>();


            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider());
            })
                .ConfigureApiBehaviorOptions(o =>
                {
                    o.InvalidModelStateResponseFactory = m => throw new ModelValidationException(m.ModelState);
                    o.SuppressMapClientErrors = true;
                })
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    o.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            //jwt token
            services.AddJwtAuthentication(Configuration);

            //swagger
            services.AddSwaggerConfiguration();

            //app services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestAccountService, TestAccountService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddHttpContextAccessor();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StoreDbContext db)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHsts();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.EnablePersistAuthorization();
                opt.ShowCommonExtensions();
                opt.ShowExtensions();
                opt.EnableDeepLinking();
                //opt.DisplayRequestDuration();
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Store API");
            });


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