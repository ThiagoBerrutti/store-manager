using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreAPI.Exceptions;
using System.Net;

namespace StoreAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            int statusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problemDetails = null;

            if (exception is ExceptionWithProblemDetails exceptionWithProblemDetails)
            {
                problemDetails = exceptionWithProblemDetails.ProblemDetails;

                switch (exceptionWithProblemDetails)
                {
                    case AppException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
                            break;
                        }

                    case AppValidationException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
                            break;
                        }

                    case DomainNotFoundException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.NotFound;
                            break;
                        }

                    case IdentityException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
                            break;
                        }

                    case IdentityNotFoundException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.NotFound;
                            break;
                        }

                    case InfrastructureException _:
                        {
                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
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
                    Status = statusCode,
                    Title = "Unexpected error",
                    Type = exception.GetType().Name,
                    Detail = exception.Message
                };
            }

            context.HttpContext.Response.ContentType = "application/problem+json";
            context.HttpContext.Response.StatusCode = statusCode;

            context.Result = new ObjectResult(problemDetails);
        }
    }
}