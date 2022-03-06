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
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using StoreAPI.Exceptions;
using StoreAPI.Extensions;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Persistence;
using StoreAPI.Persistence.Data;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Services;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreAPI
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
            var connectionStringBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SomeeDbSQLServer"))
            {
                UserID = Configuration["SomeeDbSQLServer:UserId"],
                Password = Configuration["SomeeDbSQLServer:DbPassword"]
            };
            var connectionString = connectionStringBuilder.ConnectionString;

            services.AddDbContext<StoreDbContext>(options =>
            {
                options
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging();
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
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                  "\r\n\r\nEnter your token in the text input below. "
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });

                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Store API",
                        Version = "v1",
                        Description = "Demo API for inventory data management using JWT authentication",
                        Contact = new OpenApiContact
                        {
                            Name = "Thiago Berrutti",
                            Email = "thiagoberrutti@gmail.com"
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);

                options.EnableAnnotations();

                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                options.OperationFilter<AddResponseHeadersFilter>();

                //options.SchemaFilter<EnumTypesSchemaFilter>(xmlPath);

                //options.OperationFilter<FromQueryModelFilter>();

                //options.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date" });
            });


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

            services.AddScoped<ProductSeed>();
            //services.AddTransient<ExceptionWithProblemDetails>();
            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddHttpContextAccessor();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductSeed pSeed)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHsts();

            Task.Run(async () => await pSeed.Seed());//.Wait();

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