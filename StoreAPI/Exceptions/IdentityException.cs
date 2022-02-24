using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Exceptions
{
    public class IdentityException : ExceptionWithProblemDetails
    {
        public IdentityException()
        {            
        }

        public IdentityException(IdentityResult identityResult)
        {
            SetErrors(identityResult.Errors.Select(e => e.Description));
        }

        public IdentityException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}