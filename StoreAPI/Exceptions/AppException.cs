using Microsoft.AspNetCore.Mvc;
using System;

namespace StoreAPI.Exceptions
{
    public class AppException : ExceptionWithProblemDetails
    {
        public AppException()
        {
        }

        public AppException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}