using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SalesAPI.Exceptions.Domain
{
    public class IdentityNotFoundException : Exception
    {
        public IdentityNotFoundException(string message) : base(message)
        {
        }
    }
}