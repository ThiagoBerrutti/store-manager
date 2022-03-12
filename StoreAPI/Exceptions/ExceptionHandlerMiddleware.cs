using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreAPI.Exceptions
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }


        public Task HandleException(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problemDetails = null;
                        
            if (exception is ExceptionWithProblemDetails exceptionWithProblemDetails)
            {
                problemDetails = exceptionWithProblemDetails.ProblemDetails;

                switch (exceptionWithProblemDetails)
                {
                    case InfrastructureException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
                            break;
                        }

                    case ModelValidationException modelValidationException:
                        {
                            modelValidationException.SetTitle(problemDetails.Title ?? "Model validation error");
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
                            break;
                        }
                }

                if (!problemDetails.Status.HasValue)
                {
                    exceptionWithProblemDetails.SetStatus(statusCode);
                }
            }

            if (problemDetails == null)
            {
                problemDetails = new ProblemDetails
                {
                    Detail = exception.Message,
                    Instance = context.Request.Path,
                    Status = statusCode,
                    Title = "Unexpected error",
                    Type = exception.GetType().Name
                };
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var jsonResponse = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}