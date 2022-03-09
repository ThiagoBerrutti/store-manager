using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreAPI.Infra
{
    public class ErrorObjectResult : ObjectResult
    {
        public ErrorObjectResult(object value) : base(value)
        {
            if (value is ProblemDetails asProblemDetails)
            {
                StatusCode = asProblemDetails.Status ?? StatusCodes.Status500InternalServerError;
            }
        }

        public ErrorObjectResult(object value, int? statusCode) : this(value)
        {
            if (statusCode.HasValue)
            {
                StatusCode = statusCode;
            }
        }
    }
}