using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Exceptions
{
    public class InfrastructureException : ExceptionWithProblemDetails
    {
        public InfrastructureException()
        {
        }

        public InfrastructureException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}