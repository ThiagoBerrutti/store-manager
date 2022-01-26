using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Exceptions
{
    public class StockException : Exception
    {
        public StockException(string message) : base(message)
        {
        }
    }
}
