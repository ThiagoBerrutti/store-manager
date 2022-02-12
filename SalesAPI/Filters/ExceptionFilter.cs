using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SalesAPI.Exceptions;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Models;
using System.Linq;
using System.Net;

namespace SalesAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            switch (exception)
            {
                case ApplicationException applicationException:
                    {
                        var problemDetails = applicationException.ProblemDetails;

                        int statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
                        if (!problemDetails.Status.HasValue) 
                        { 
                            applicationException.SetStatus(statusCode);
                        }

                        context.Result = new BadRequestObjectResult(problemDetails);
                        context.HttpContext.Response.StatusCode =  statusCode;
                        context.HttpContext.Response.ContentType = "application/problem+json";
                        break;
                    }

                case DomainNotFoundException domainNotFoundException:
                    {
                        //var statusCode = (int)HttpStatusCode.NotFound;

                        //var errorModel = new ErrorModel(entityNotFoundException, statusCode);
                        //var json = JsonConvert.SerializeObject(errorModel);

                        //context.Result = new NotFoundObjectResult(errorModel);
                        //context.HttpContext.Response.StatusCode = statusCode;
                        ////
                        var problemDetails = domainNotFoundException.ProblemDetails;
                        int statusCode;
                        
                        if (!problemDetails.Status.HasValue)
                        {
                            statusCode = (int)HttpStatusCode.NotFound;
                            domainNotFoundException.SetStatus(statusCode);                            
                        }
                        else
                        {
                            statusCode = problemDetails.Status.Value;
                        }

                        context.Result = new NotFoundObjectResult(problemDetails);
                        context.HttpContext.Response.StatusCode = statusCode;
                        context.HttpContext.Response.ContentType = "application/problem+json";
                        break;
                    }

                case IdentityException identityException:
                    {
                        var errors = identityException.Errors.Select(e => e.Description);
                        //var errors = ((IEnumerable<IdentityError>)identityException.Data["Errors"])
                        //                .Select(e => e.Description);

                        var statusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorModel(identityException, statusCode, errors);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new BadRequestObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;

                        //problemDetail.Extensions.Add("errors", errors);
                        //problemDetail.Status = statusCode;
                        //problemDetail.Title = identityException.Message;
                        //problemDetail.Detail =


                        break;
                    }

                case IdentityNotFoundException identityNotFoundException:
                    {
                        var statusCode = (int)HttpStatusCode.NotFound;

                        var errorModel = new ErrorModel(identityNotFoundException, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new NotFoundObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                case InfrastructureException infraException:
                    {
                        var statusCode = (int)HttpStatusCode.InternalServerError;

                        var errorModel = new ErrorModel(infraException, statusCode, infraException.Errors);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new ObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                case StockException stockException:
                    {
                        var statusCode = (int)HttpStatusCode.BadRequest;

                        stockException.SetStatus(statusCode);
                        var problemDetails = stockException.ProblemDetails;
                        
                        var json = JsonConvert.SerializeObject(problemDetails);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = statusCode;
                        context.HttpContext.Response.ContentType = "application/problem+json";
                        break;
                    }

                //case ValidationException validationException:
                //    {
                //        var statusCode = (int)HttpStatusCode.BadRequest;
                //        var validationProblemDetails = new ValidationProblemDetails();
                //        problemDetail.Extensions["Errors"] = validationException.Errors;

                //        //var errorModel = new ErrorModel(stockException, statusCode);
                //        var json = JsonConvert.SerializeObject(errorModel);

                //        context.Result = new BadRequestObjectResult(errorModel);
                //        context.HttpContext.Response.StatusCode = statusCode;
                //        break;
                //    }



                default:
                    {
                        var statusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorModel(exception, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new BadRequestObjectResult("DEU RUIM");
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }
            }
        }
    }
}