using System;
using System.Collections.Generic;

namespace SalesAPI.Exceptions
{
    public class InfrastructureException : ExceptionWithProblemDetails
    {
        public IEnumerable<string> Errors;

        public InfrastructureException()
        {
        }        
    }
}