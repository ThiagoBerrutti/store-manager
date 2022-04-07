using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Identity;
using System.Linq;

namespace StoreAPI.Services.Communication
{
    public class FailedServiceResponse<T> : ServiceResponse<T>
    {
        public object ErrorData { get; set; }

        public FailedServiceResponse()
        {
            Success = false;
        }



        public FailedServiceResponse(ServiceResponse serviceResponse) : this()
        {
            if (serviceResponse is ServiceResponse<T> asServiceT)
            {
                Data = asServiceT.Data;
            }

            Error = serviceResponse.Error;
        }


        public FailedServiceResponse(ProblemDetails error) : this()
        {
            HandleErrorParameter(error);
        }



        public FailedServiceResponse(IdentityResult error) : this()
        {
            HandleErrorParameter(error);
        }


        public FailedServiceResponse(ValidationResult error) : this()
        {
            HandleErrorParameter(error);
        }

        public FailedServiceResponse(Microsoft.AspNetCore.Identity.SignInResult signInResult, User user = null) : this()
        {
            HandleErrorParameter(signInResult, user);
        }


        public FailedServiceResponse<T> SetData(T data)
        {
            Data = data;

            return this;
        }


        public FailedServiceResponse<T> SetTitle(string title)
        {
            Error.Title = title;

            return this;
        }


        public FailedServiceResponse<T> SetDetail(string detail)
        {
            Error.Detail = detail;

            return this;
        }


        public FailedServiceResponse<T> AddToExtensionsErrors(object errors)
        {
            if (errors != null)
            {
                Error.Extensions[ErrorKey] = errors;
            }

            return this;
        }


        public FailedServiceResponse<T> SetExtensions(string key, object extension)
        {
            if (extension != null)
            {
                Error.Extensions.Add(key, extension);
            }

            return this;
        }


        public FailedServiceResponse<T> SetInstance(string instance)
        {
            SetInstance(instance);
            Error.Instance = instance;

            return this;
        }


        public FailedServiceResponse<T> SetStatus(int statusCode)
        {
            Error.Status = statusCode;

            return this;
        }


        public FailedServiceResponse<T> SetErrorData(object errorData)
        {
            ErrorData = errorData;
            Success = false;

            return this;
        }


        private void HandleErrorParameter(IdentityResult error)
        {
            if (!error.Succeeded)
            {
                AddToExtensionsErrors(error.Errors.Select(e => e.Description));
            }
        }

        private void HandleErrorParameter(ValidationResult error)
        {
            if (!error.IsValid)
            {
                SetTitle("Validation error");
                AddToExtensionsErrors(error.Errors.Select(e => e.ErrorMessage));
                SetDetail($"Invalid data. See '{ErrorKey}' for more details");
                SetStatus(400);
            }
        }


        private void HandleErrorParameter(ProblemDetails error)
        {
            Error = error;
        }

        private void HandleErrorParameter(Microsoft.AspNetCore.Identity.SignInResult error, User user = null)
        {
            if (!error.Succeeded)
            {
                if (error.IsLockedOut)
                {
                    if (user is null)
                    {
                        AddToExtensionsErrors($"User locked out" +
                            $"{(user != null ? $" until {user.LockoutEnd:G}" : "")}");
                    }
                }

                if (error.IsNotAllowed)
                {
                    AddToExtensionsErrors($"User not allowed");
                }
            }
        }
    }
}
