using Microsoft.AspNetCore.Mvc;
using System;

namespace SalesAPI.Exceptions
{
    public class ApplicationException : ExceptionWithProblemDetails
    {
        public ApplicationException()   
        {
        }
    }
}