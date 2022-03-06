using System;
using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class UserParametersDto : QueryStringParameterDto
    {
        /// <summary>
        /// Returns only users whose DateOfBirth is later than this date.
        /// <br/>
        ///     <details>
        ///             <summary>See more:</summary>
        ///             <para>Value can be any valid DateTime format (en-US)</para>
        ///             <br/>
        ///             <para>Examples:
        /// - 2000-01-01
        /// - Saturday, 1 Jan 2000
        /// - 1/1/2000
        /// - Jan 2000
        ///
        /// etc..
        ///             </para>
        ///             <para><small><i>[See: [Date and Time format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings)]</i></small></para>
        ///     </details>
        /// </summary>
        /// <example>Mon, 1 Jan 1990</example>
        public DateTime? MinDateOfBirth { get; set; }


        /// <summary>
        /// Returns only users whose DateOfBirth is earlier than this date.
        /// <br/>
        ///     <details>
        ///             <summary>See more:</summary>
        ///             <para>Value can be any valid DateTime format (en-US)</para>
        ///             <br/>
        ///             <para>Examples:
        /// - 2000-01-01
        /// - Saturday, 1 Jan 2000
        /// - 1/1/2000
        /// - Jan 2000
        ///
        /// etc..
        ///             </para>
        ///             <para><small><i>[See: [Date and Time format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings)]</i></small></para>
        ///         </details>
        /// </summary>
        /// <example>2008-04-10T06:30:00</example>
        public DateTime? MaxDateOfBirth { get; set; }

        /// <summary>
        /// Returns only users at least that age
        /// </summary>
        /// <example>20</example>
        public int? MinAge { get; set; }

        /// <summary>
        /// Returns only users at most that age
        /// </summary>
        /// <example>35</example>
        public int? MaxAge { get; set; }

        /// <summary>
        /// Returns only users assigned to these roles.
        /// </summary>
        public List<int> RoleId { get; set; } = new List<int>();

        /// <summary>
        /// Returns only users whose username contains this string
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// Returns only users whose first name or last name contains this string
        /// </summary>
        /// <example>ad</example>
        public string Name { get; set; } = "";


        // Search only for the latest date between MaxDateOfBirth and the date of the MaxAge
        public DateTime? LatestDateToSearch()
        {
            if (MinAge.HasValue)
            {
                var maximumDateByAge = DateTime.UtcNow.AddYears(-MinAge.Value);
                if (MaxDateOfBirth.HasValue && (MaxDateOfBirth > maximumDateByAge))
                {
                    return MaxDateOfBirth;
                }

                return maximumDateByAge;
            }

            if (MaxDateOfBirth.HasValue)
            {
                return MaxDateOfBirth;
            }

            return DateTime.UtcNow;
        }

        // Returns the earliest date between MinDateOfBirth and the earliest date possible where the user Age = MaxAge (a day from entering another age)
        public DateTime? EarliestDateToSearch()
        {
            if (MaxAge.HasValue)
            {
                var minimumDateByAge = DateTime.UtcNow
                                                .AddYears(-(MaxAge.Value + 1))  // to include the year before completing another birthday
                                                .AddDays(1);                    
                if (MinDateOfBirth.HasValue && (MinDateOfBirth < minimumDateByAge))
                {
                    return MinDateOfBirth;
                }

                return minimumDateByAge;
            }

            if (MinDateOfBirth.HasValue)
            {
                return MinDateOfBirth;
            }

            return new DateTime();
        }
    }
}