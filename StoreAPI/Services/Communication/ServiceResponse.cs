using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Services.Communication
{
    public class ServiceResponse
    {
        public bool Success { get; protected set; }
        public ProblemDetails Error { get; protected set; } = new ProblemDetails { Status = StatusCodes.Status400BadRequest };
        public const string ErrorKey = "errors";

        public ServiceResponse()
        {
            Success = true;
        }


        public IActionResult GenerateErrorActionResult()
        {
            switch (Error.Status)
            {
                case 404:
                    {
                        return new NotFoundObjectResult(Error);
                    }
                case 400:
                    {
                        return new BadRequestObjectResult(Error);
                    }
                default:
                    {
                        var action = new ObjectResult(Error)
                        {
                            StatusCode = 500
                        };

                        return action;
                    }
            }
        }
    }
}