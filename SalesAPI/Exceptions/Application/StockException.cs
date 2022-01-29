using System;

namespace SalesAPI.Exceptions
{
    public class StockException : Exception
    {
        public StockException(string message) : base(message)
        {
        }
    }
}