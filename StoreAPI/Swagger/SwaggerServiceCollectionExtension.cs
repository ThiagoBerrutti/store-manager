using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace StoreAPI.Swagger
{
    public static class SwaggerServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
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
            });
        }
    }
}