using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace SalesAPI.Models
{
    public class ErrorModelFull : IErrorViewModel
    {
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Exception InnerException { get; set; }
        public string StackTrace { get; set; }

        public ErrorModelFull(int statusCode, string type, string message, IEnumerable<string> errors, Exception innerException, string stackTrace)
        {
            StatusCode = statusCode;
            Type = type;
            Message = message;
            Errors = errors ?? new List<string>();
            InnerException = innerException;
            StackTrace = stackTrace;
        }

        public ErrorModelFull(Exception ex) 
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = new List<string>();
            InnerException = ex.InnerException;
            StackTrace = ex.StackTrace;
        }

        public ErrorModelFull(Exception ex, int statusCode) 
        {
            StatusCode = statusCode;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = new List<string>();
            InnerException = ex.InnerException;
            StackTrace = ex.StackTrace;
        }

        public ErrorModelFull(Exception ex, IEnumerable<string> errors)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = errors;
            InnerException = ex.InnerException;
            StackTrace = ex.StackTrace;
        }


        public ErrorModelFull(Exception ex, int statusCode, IEnumerable<string> errors) 
        {
            StatusCode = statusCode;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = errors;
            InnerException = ex.InnerException;
            StackTrace = ex.StackTrace;
        }


        


        public ErrorModelFull()
        {

        }
    }
}
