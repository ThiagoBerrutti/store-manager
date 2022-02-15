//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SalesAPI.Exceptions;
//using SalesAPI.Exceptions.Domain;
//using System.Net;
//using System.Threading.Tasks;

//namespace SalesAPI.Controllers
//{
//    [ApiController]
//    [ApiExplorerSettings(IgnoreApi = true)]
//    [AllowAnonymous]
//    public class ErrorController : ControllerBase
//    {
//        [Route("/Error")]
//        public IActionResult Error()
//        {
//            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
//            var exception = exceptionHandlerPathFeature.Error;
//            int statusCode = (int)HttpStatusCode.BadRequest;
//            object response;

//            if (exception is ExceptionWithProblemDetails exceptionWithProblemDetails)
//            {
//                var problemDetails = exceptionWithProblemDetails.ProblemDetails;

//                switch (exceptionWithProblemDetails)
//                {
//                    case ApplicationException _:
//                        {
//                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
//                            break;
//                        }

//                    case DomainNotFoundException _:
//                        {
//                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.NotFound;
//                            break;
//                        }

//                    case IdentityException _:
//                        {
//                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;
//                            break;
//                        }

//                    case IdentityNotFoundException _:
//                        {
//                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.NotFound;
//                            break;
//                        }

//                    case InfrastructureException _:
//                        {
//                            statusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
//                            break;
//                        }
//                }

//                if (!problemDetails.Status.HasValue)
//                {
//                    exceptionWithProblemDetails.SetStatus(statusCode);
//                }

//                response = problemDetails;
//            }
//            else
//            {
//                statusCode = (int)HttpStatusCode.BadRequest;
//                response = new ProblemDetails { Title = "Unexpected error", Detail = "Something unexpected happened", Status = statusCode };
//            }

//            HttpContext.Response.ContentType = "application/problem+json";
//            HttpContext.Response.StatusCode = statusCode;
//            //HttpContext.Response.WriteAsync("AA");

//            return new ObjectResult(response);
//        }
//    }
//}