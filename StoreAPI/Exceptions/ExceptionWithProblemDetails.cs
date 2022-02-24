using Microsoft.AspNetCore.Mvc;
using System;

namespace StoreAPI.Exceptions
{
    public class ExceptionWithProblemDetails : Exception
    {
        public const string ErrorKey = "errors";
        public ProblemDetails ProblemDetails { get; }

        public ExceptionWithProblemDetails()
        {
            ProblemDetails = new ProblemDetails();
        }

        public ExceptionWithProblemDetails(ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;

            if (problemDetails is ValidationProblemDetails validationProblemDetails)
            {
                SetErrors(validationProblemDetails.Errors);
            }
        }


        public ExceptionWithProblemDetails SetTitle(string title)
        {
            ProblemDetails.Title = title;
            return this;
        }


        public ExceptionWithProblemDetails SetDetail(string detail)
        {
            ProblemDetails.Detail = detail;
            return this;
        }


        public ExceptionWithProblemDetails SetErrors(object errors)
        {
            if (errors != null)
            {
                ProblemDetails.Extensions[ErrorKey] = errors;
            }

            return this;
        }


        public ExceptionWithProblemDetails SetExtensions(string key, object extension)
        {
            if (extension != null)
            {
                ProblemDetails.Extensions.Add(key, extension);
            }

            return this;
        }


        public ExceptionWithProblemDetails SetInstance(string instance)
        {
            ProblemDetails.Instance = instance;
            return this;
        }


        public ExceptionWithProblemDetails SetStatus(int statusCode)
        {
            ProblemDetails.Status = statusCode;
            return this;
        }
    }
}