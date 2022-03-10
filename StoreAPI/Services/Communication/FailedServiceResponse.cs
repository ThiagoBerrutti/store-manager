using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Identity;
using System.Linq;

namespace StoreAPI.Services.Communication
{
    public class FailedServiceResponse : ServiceResponse
    {
        public FailedServiceResponse()
        {
            Success = false;
        }

        public FailedServiceResponse(ServiceResponse error)
        {
            Error = error.Error;
        }


        public FailedServiceResponse(ProblemDetails error) : this()
        {
            HandleErrorParameter(error);
        }

        public FailedServiceResponse(IdentityResult error) : this()
        {
            HandleErrorParameter(error);
        }

        public FailedServiceResponse(Microsoft.AspNetCore.Identity.SignInResult error, User user = null) : this()
        {
            HandleErrorParameter(error, user);
        }


        public FailedServiceResponse(ValidationResult error) : this()
        {
            HandleErrorParameter(error);
        }



        public FailedServiceResponse SetTitle(string title)
        {
            Error.Title = title;
            Success = false;

            return this;
        }


        public FailedServiceResponse SetDetail(string detail)
        {
            Error.Detail = detail;
            Success = false;

            return this;
        }


        public FailedServiceResponse AddToExtensionsErrors(object errors)
        {
            Success = false;

            if (errors != null)
            {
                Error.Extensions[ErrorKey] = errors;
            }

            return this;
        }


        public FailedServiceResponse SetExtensions(string key, object extension)
        {
            if (extension != null)
            {
                Error.Extensions.Add(key, extension);
            }

            Success = false;

            return this;
        }


        public FailedServiceResponse SetInstance(string instance)
        {
            Error.Instance = instance;
            Success = false;

            return this;
        }


        public FailedServiceResponse SetStatus(int statusCode)
        {
            Error.Status = statusCode;
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
                    AddToExtensionsErrors($"User locked out" +
                        $"{(user != null ? $" until {user.LockoutEnd:G}" : "")}");
                }

                if (error.IsNotAllowed)
                {
                    AddToExtensionsErrors($"User not allowed");
                }
            }
        }
    }
}