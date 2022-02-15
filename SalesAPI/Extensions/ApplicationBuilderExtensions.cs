using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SalesAPI.Exceptions;
using System.Net;
using System.Text.Json;

namespace SalesAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        //global exception handler. Mvc exceptions are handled on ExceptionFilter
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var statusCode = (int)HttpStatusCode.BadRequest;
                    ProblemDetails problemDetails = null;

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerFeature.Error;

                    if (exception != null)
                    {
                        // implementations that set problemDetails a new value accordingly to the exception, if needed. Controller exceptions are handled on another class, ExceptionFilter.
                    }

                    if (problemDetails == null)
                    {
                        var problemDetailsFactory = new CustomProblemDetailsFactory();
                        problemDetails = problemDetailsFactory
                            .CreateProblemDetails(context, statusCode, "Unexpected error", exception.GetType().Name, exception.Message, context.Request.Path);                        
                    }

                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = statusCode;

                    var jsonResponse = JsonSerializer.Serialize(problemDetails);

                    await context.Response.WriteAsync(jsonResponse);
                });
            });

            return app;
        }
    }
}