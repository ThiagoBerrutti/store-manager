using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Exceptions
{
    public class IdentityNotFoundException : ExceptionWithProblemDetails
    {
        public IdentityNotFoundException()
        {
        }

        public IdentityNotFoundException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}