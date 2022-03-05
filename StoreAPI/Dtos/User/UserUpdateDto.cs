using System;

namespace StoreAPI.Dtos
{
    /// <summary>
    /// User data to be persisted on update operations
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// User first name
        /// </summary>
        /// <example>John</example>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        /// <example>Doe</example>
        public string LastName { get; set; }

        /// <summary>
        /// User date of birth
        /// <br/>
        ///      <details>
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
        /// <example>22-02-1992</example>
        public DateTime? DateOfBirth { get; set; }
    }
}