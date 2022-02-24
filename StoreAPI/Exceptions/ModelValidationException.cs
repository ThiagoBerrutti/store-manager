using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreAPI.Exceptions
{
    public class ModelValidationException : ExceptionWithProblemDetails
    {
        public ModelValidationException()
        {
        }

        public ModelValidationException(ModelStateDictionary modelState) : base(new ValidationProblemDetails(modelState))
        {
        }
    }
}