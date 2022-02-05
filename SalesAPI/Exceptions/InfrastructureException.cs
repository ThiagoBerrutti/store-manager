using System;
using System.Collections.Generic;

namespace SalesAPI.Exceptions
{
    public class InfrastructureException : Exception
    {
        public IEnumerable<string> Errors;

        public InfrastructureException(string message) : base(message)
        {
        }

        public InfrastructureException(string message, Exception e) 
            : base(message)
        {
            Errors = new List<string> { e.GetType().Name + ": " + e.Message };
        }

        
    }
}