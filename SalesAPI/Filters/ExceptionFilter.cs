using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SalesAPI.Exceptions;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Models;
using System;
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
            var tName = exception.GetType().Name + " :\n";


            switch (exception)
            {
                case Exceptions.ApplicationException applicationException:
                    {
                        string json = tName + JsonConvert.SerializeObject(applicationException.Message);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }


                case DomainException domainException:
                    {
                        string json = tName + JsonConvert.SerializeObject(domainException.Message);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                case DomainNotFoundException domainNotFoundException:
                    {
                        string json = tName + JsonConvert.SerializeObject(domainNotFoundException.Message);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    }

                case EntityNotFoundException entityNotFoundException:
                    {
                        string json = tName + JsonConvert.SerializeObject(entityNotFoundException.Message);

                        context.Result = new NotFoundObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    }

                case IdentityException identityException:
                    {
                        IEnumerable<string> errors = identityException.Errors.Select(e => e.Description );
                       
                        var error = GenerateJsonWithInnerAndStackTrace(identityException,errors);
                        var json = JsonConvert.SerializeObject(error);
                        //string json = tName + JsonConvert.SerializeObject(identityException.Message) + "\n" +
                        //    JsonConvert.SerializeObject(identityException.Errors);


                        context.Result = new BadRequestObjectResult(error);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                case InfrastructureException infraException:
                    {
                        string json = tName + JsonConvert.SerializeObject(infraException.Message);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    }

                case StockException stockException:
                    {
                        string json = tName + JsonConvert.SerializeObject(stockException.Message);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                default:
                    {
                        string json = tName + JsonConvert.SerializeObject(exception.Message) +
                            "\nInner Exception : \n" + exception.InnerException?.Message +
                            "\nStack Trace : \n" + exception.StackTrace;

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    }
            }
        }

        private ErrorModel GenerateJsonWithInnerAndStackTrace(Exception e, IEnumerable<string> errors)
        {
           

            var error = new ErrorModel(e, errors);
                
            //var json = $"Error : {e.GetType().Name}\n";
            //json += $"Message : {e.Message}";
            //json += ((errors != null && errors.Count() > 0) ?
            //   $"Errors : {errors.Select(e => e.ToString()).Aggregate((sums, y) => y + "\n")}" : "");
            //json += ((e.InnerException != null) ?
            //   $"InnerException : {e.InnerException?.Message}" : "");
            //json += $"StackTrace : {e.StackTrace}";

            return error;
        }

        private ErrorModel GenerateJsonWithInnerAndStackTrace(Exception e)
        {
            return GenerateJsonWithInnerAndStackTrace(e, null);
        }

    }
}
