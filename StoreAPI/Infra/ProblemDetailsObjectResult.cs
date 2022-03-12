using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Infra
{
    public class ProblemDetailsObjectResult : ObjectResult
    {
        public ProblemDetailsObjectResult(object value) : base(value)
        {
            if (value is ProblemDetails asProblemDetails)
            {
                StatusCode = asProblemDetails.Status ?? StatusCodes.Status500InternalServerError;
            }
        }

        public ProblemDetailsObjectResult(object value, int? statusCode) : this(value)
        {
            if (statusCode.HasValue)
            {
                StatusCode = statusCode;
            }
        }
    }
}