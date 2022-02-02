using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Models
{
    public class ErrorModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public Exception InnerException { get; set; }
        public string StackTrace { get; set; }

        public ErrorModel(string type, string message, IEnumerable<string> errors, Exception innerException, string stackTrace)
        {
            Type = type;
            Message = message;
            Errors = errors;
            InnerException = innerException;
            StackTrace = stackTrace;
        }

        public ErrorModel(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            InnerException = ex.InnerException;
            StackTrace = ex.StackTrace;
        }

        public ErrorModel(Exception ex, IEnumerable<string> errors)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = errors;
            InnerException = ex.InnerException;
            StackTrace = ex.StackTrace;
        }

        public ErrorModel()
        {

        }
    }
}
