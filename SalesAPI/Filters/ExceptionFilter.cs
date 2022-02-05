using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SalesAPI.Exceptions;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Models;
using System.Collections.Generic;
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
                        var statusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorModelShort(applicationException, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new BadRequestObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }
               
                case DomainNotFoundException entityNotFoundException:
                    {
                        var statusCode = (int)HttpStatusCode.NotFound;

                        var errorModel = new ErrorModelShort(entityNotFoundException, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new NotFoundObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                case IdentityException identityException:
                    {
                        IEnumerable<string> errors = identityException.Errors.Select(e => e.Description);

                        var statusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorModelShort(identityException, statusCode, errors);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new BadRequestObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                case IdentityNotFoundException identityNotFoundException:
                    {
                        var statusCode = (int)HttpStatusCode.NotFound;

                        var errorModel = new ErrorModelShort(identityNotFoundException, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new NotFoundObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                case InfrastructureException infraException:
                    {
                        var statusCode = (int)HttpStatusCode.InternalServerError;

                        var errorModel = new ErrorModelShort(infraException, statusCode, infraException.Errors);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new ObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                case StockException stockException:
                    {
                        var statusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorModelShort(stockException, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new BadRequestObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }

                default:
                    {
                        var statusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorModelShort(exception, statusCode);
                        var json = JsonConvert.SerializeObject(errorModel);

                        context.Result = new BadRequestObjectResult(errorModel);
                        context.HttpContext.Response.StatusCode = statusCode;
                        break;
                    }
            }
        }
    }
}