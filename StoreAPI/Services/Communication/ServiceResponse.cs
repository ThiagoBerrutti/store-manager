using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace StoreAPI.Services
{
    public class ServiceResponse<T>
    {
        public T Data { get; private set; }
        public bool Success { get; protected set; }
        public ProblemDetails Error { get; protected set; } = new ProblemDetails { Status = StatusCodes.Status400BadRequest };
        public const string ErrorKey = "errors";

        public bool HasData => Data != null;

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(T result) : this()
        {
            Data = result;
        }

        public ServiceResponse(ProblemDetails error)
        {
            Error = error;
            Success = false;
        }

        public ServiceResponse(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                Success = false;
                SetTitle("Validation error");
                SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
                SetStatus(400);
            }
        }


        //public ServiceResponse(IdentityResult identityResult)
        //{
        //    if (!identityResult.Succeeded)
        //    {
        //        Success = false;
        //        SetErrors(identityResult.Errors.Select(e => e.Description));
        //        SetStatus(400);
        //    }
        //}

        public ServiceResponse(T data, ProblemDetails error) : this(error)
        {
            Data = data;
        }


        public ServiceResponse<T> SetTitle(string title)
        {
            Error.Title = title;
            Success = false;

            return this;
        }


        public ServiceResponse<T> SetDetail(string detail)
        {
            Error.Detail = detail;
            Success = false;

            return this;
        }


        public ServiceResponse<T> SetErrors(object errors)
        {
            if (errors != null)
            {
                Error.Extensions[ErrorKey] = errors;
            }

            Success = false;

            return this;
        }


        public ServiceResponse<T> SetExtensions(string key, object extension)
        {
            if (extension != null)
            {
                Error.Extensions.Add(key, extension);
            }

            Success = false;

            return this;
        }


        public ServiceResponse<T> SetInstance(string instance)
        {
            Error.Instance = instance;
            Success = false;

            return this;
        }


        public ServiceResponse<T> SetStatus(int statusCode)
        {
            Error.Status = statusCode;
            Success = false;

            return this;
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