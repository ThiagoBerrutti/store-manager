using System;
using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    /// <summary>
    /// User data
    /// </summary>
    public class UserReadDto 
    {
        /// <summary>
        /// User Id
        /// </summary>
        /// <example>123</example>
        public int Id { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        /// <example>johndoe123</example>
        public string UserName { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        /// <example>John Doe</example>
        public string FullName { get; set; }

        /// <summary>
        /// User age
        /// </summary>
        /// <example>30</example>
        public int Age { get; set; }

        /// <summary>
        /// User assigned roles
        /// </summary>
        public List<RoleReadDto> Roles { get; set; }

    }
}