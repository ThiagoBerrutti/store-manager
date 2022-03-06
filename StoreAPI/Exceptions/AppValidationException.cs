using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.Exceptions
{
    public class AppValidationException : ExceptionWithProblemDetails
    {
        public AppValidationException()
        {
        }

        public AppValidationException(ValidationResult validationResult)
        {
            SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public AppValidationException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}