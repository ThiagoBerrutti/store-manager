using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalesAPI.Exceptions;
using SalesAPI.Exceptions.Domain;
using System.Net;

namespace SalesAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            int statusCode = (int)HttpStatusCode.BadRequest;
            object response;

            if (exception is ExceptionWithProblemDetails exceptionWithProblemDetails)
            {
                var problemDetails = exceptionWithProblemDetails.ProblemDetails;

                switch (exceptionWithProblemDetails)
                {
                    case ApplicationException _:
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

                response = problemDetails;
            }
            else
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new ProblemDetails { Title = "Unexpected error", Detail = "Something unexpected happened", Status = statusCode };
            }

            context.HttpContext.Response.ContentType = "application/problem+json";
            context.HttpContext.Response.StatusCode = statusCode;

            context.Result = new ObjectResult(response);
        }
    }
}