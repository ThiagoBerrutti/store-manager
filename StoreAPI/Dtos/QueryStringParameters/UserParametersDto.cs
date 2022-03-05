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


        public DateTime? LatestDateToSearch()
        {
            var minAgeDoB = DateTime.UtcNow.AddYears(-MinAge.GetValueOrDefault());
            var result = MinAge.HasValue && (MaxDateOfBirth > minAgeDoB) ? MaxDateOfBirth : minAgeDoB;

            return result;
        }


        public DateTime? EarliestDateToSearch()
        {
            var minAgeDoB = DateTime.UtcNow.AddYears(-MaxAge.GetValueOrDefault());
            var result = MinAge.HasValue && (MinDateOfBirth < minAgeDoB) ? MinDateOfBirth : minAgeDoB;

            return result;
        }
    }
}