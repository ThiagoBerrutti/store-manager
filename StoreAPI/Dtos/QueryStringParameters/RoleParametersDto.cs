using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    /// <summary>
    /// Query string parameters for product search filtering
    /// </summary>
    public class RoleParametersDto : QueryStringParameterDto
    {
        /// <summary>
        /// Select only roles whose name contains this string
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Select only roles assigned to these users.
        /// </summary>
        public List<int> UserId { get; set; } = new List<int>();
    }
}