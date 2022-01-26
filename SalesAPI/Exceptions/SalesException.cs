using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Exceptions
{
    public class SalesException : Exception
    {
        public SalesException(string message) : base(message)
        {                                
        }
    }
}
