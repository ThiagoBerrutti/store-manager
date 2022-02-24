using StoreAPI.Exceptions;
using System;

namespace StoreAPI.Helpers
{
    public static class AgeCalculator
    {
        public static int Calculate(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
            {
                throw new ArgumentNullException("Cannot calculate age with null date value");
            }
            var date = dateOfBirth.Value;
            var now = DateTime.UtcNow;

            var years = now.Year - date.Year;

            if (now.Month <= date.Month)
            {
                if (now.Day < date.Day)
                {
                    years--;
                }
            }

            return years;
        }
    }
}