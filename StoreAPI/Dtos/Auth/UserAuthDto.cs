using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    /// <summary>
    /// User data from an authentication operation
    /// </summary>
    public class UserAuthDto
    {
        /// <summary>
        /// User id
        /// </summary>
        /// <example>7</example>
        public int Id { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        /// <example>johndoe123</example>
        public string UserName { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public List<RoleReadDto> Roles { get; set; }
    }
}