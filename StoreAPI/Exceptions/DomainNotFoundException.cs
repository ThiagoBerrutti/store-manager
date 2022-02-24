using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Exceptions
{
    public class DomainNotFoundException : ExceptionWithProblemDetails
    {
        public DomainNotFoundException()
        {
        }

        public DomainNotFoundException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}