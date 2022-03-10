using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Services.Communication
{
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; protected set; }

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(T result) : this()
        {
            Data = result;
        }


        public new FailedServiceResponse<T> HasFailed()
        {
            var result = new FailedServiceResponse<T>(this);

            return result;
        }

        public new FailedServiceResponse<T> HasFailed(IdentityResult error)
        {
            var result = new FailedServiceResponse<T>(error);

            return result;
        }

        public new FailedServiceResponse<T> HasFailed(ProblemDetails error)
        {
            var result = new FailedServiceResponse<T>(error);

            return result;
        }

        public new FailedServiceResponse<T> HasFailed(ValidationResult error)
        {
            var result = new FailedServiceResponse<T>(error);

            return result;
        }
    }
}