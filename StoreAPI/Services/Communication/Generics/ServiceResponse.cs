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
    }
}