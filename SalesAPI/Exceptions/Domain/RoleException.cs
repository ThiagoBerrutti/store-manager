using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Exceptions.Domain
{
    public class RoleException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }

        public RoleException(string message) : base(message)
        {
        }

        public RoleException(string message, IEnumerable<IdentityError> errors) : this(message)
        {
            Errors = errors;
        }


    }
}
