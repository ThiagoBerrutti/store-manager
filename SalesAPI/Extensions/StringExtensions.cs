using System.Collections.Generic;

namespace SalesAPI.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsRange(this string text, IEnumerable<string> searches)
        {
            foreach (string s in searches)
            {
                if (text.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}