using System;

namespace SalesAPI.Exceptions
{
    public class DomainNotFoundException : Exception
    {
        public DomainNotFoundException(string message) : base(message)
        {
        }
    }
}