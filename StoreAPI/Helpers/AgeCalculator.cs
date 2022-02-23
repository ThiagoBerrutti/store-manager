using System;

namespace StoreAPI.Helpers
{
    public static class AgeCalculator
    {
        public static int Calculate(DateTime dateOfBirth)
        {
            var now = DateTime.UtcNow;

            var years = now.Year - dateOfBirth.Year;

            if (now.Month <= dateOfBirth.Month)
            {
                if (now.Day < dateOfBirth.Day)
                {
                    years--;
                }
            }

            return years;
        }
    }
}