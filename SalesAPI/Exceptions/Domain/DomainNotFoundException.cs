using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SalesAPI.Exceptions.Domain
{
    public class DomainNotFoundException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }

        public DomainNotFoundException(string message) : base(message)
        {
        }

        public DomainNotFoundException(string message, IEnumerable<IdentityError> errors) : this(message)
        {
            Errors = errors;
        }
    }
}