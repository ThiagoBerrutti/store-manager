using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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


        public FailedServiceResponse HasFailed()
        {
            if (this is FailedServiceResponse asFailedServiceResponse)
            {
                return asFailedServiceResponse;
            }

            return new FailedServiceResponse(this);
        }


        public FailedServiceResponse HasFailed(IdentityResult error)
        {
            var result = new FailedServiceResponse(error);

            return result;
        }


        public FailedServiceResponse HasFailed(ProblemDetails error)
        {
            return new FailedServiceResponse(error);
        }

        public FailedServiceResponse HasFailed(ValidationResult error)
        {
            return new FailedServiceResponse(error);
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