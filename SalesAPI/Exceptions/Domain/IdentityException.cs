using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SalesAPI.Exceptions.Domain
{
    public class IdentityException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; } = new List<IdentityError>();

        public IdentityException(string message) : base(message)
        {
        }

        public IdentityException(string message, IEnumerable<IdentityError> errors) : this(message)
        {
            Errors = errors;
        }
    }
}