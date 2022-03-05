using StoreAPI.Helpers;
using System;

namespace StoreAPI.Dtos
{
    public class UserRegisterDto
    {
        /// <summary>
        /// User username
        /// </summary>
        /// <example>johnsmith123</example>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>secretpw123</example>
        public string Password { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        /// <example>John</example>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        /// <example>Smith</example>
        public string LastName { get; set; }


        /// <summary>
        /// User date of birth
        ///     <details>
        ///             <summary>See more:</summary>
        ///     
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
        /// <example>Mon, 1 Jan 1990</example>
        public DateTime DateOfBirth { get; set; }
    }
}