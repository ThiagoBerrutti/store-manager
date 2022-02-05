using System;
using System.Collections.Generic;
using System.Net;

namespace SalesAPI.Models
{
    public class ErrorModelShort
    {
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ErrorModelShort(int statusCode, string type, string message, IEnumerable<string> errors)
        {
            StatusCode = statusCode;
            Type = type;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public ErrorModelShort(Exception ex) 
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = new List<string>();
        }

        public ErrorModelShort(Exception ex, int statusCode)
        {
            StatusCode = statusCode;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = new List<string>();
        }

        public ErrorModelShort(Exception ex, IEnumerable<string> errors)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = errors;
        }

        public ErrorModelShort(Exception ex, int statusCode, IEnumerable<string> errors)
        {
            StatusCode = statusCode;
            Type = ex.GetType().Name;
            Message = ex.Message;
            Errors = errors;
        }

        public ErrorModelShort()
        {
        }
    }
}