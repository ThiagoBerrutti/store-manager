using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;

namespace SalesAPI.Infra
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            statusCode = (int)HttpStatusCode.BadRequest;

            ProblemDetails problemDetails = null;

            if (exceptionHandlerFeature?.Error != null)
            {
                // implementations that set 'problemDetails' a new value accordingly to the exception, if needed. Controller exceptions are handled on another class, ExceptionFilter.
            }

            if (problemDetails == null)
            {
                problemDetails = new ProblemDetails
                {
                    Detail = detail,
                    Instance = instance,
                    Status = statusCode,
                    Title = title,
                    Type = type
                };
            }

            return problemDetails;
        }


        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Title = title,
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            return problemDetails;
        }
    }
}