using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class RoleParametersDto : QueryStringParameterDto
    {
        public string Name { get; set; } = "";
        //public int? UserId { get; set; }
        /// <summary>
        /// asdfd
        /// </summary>
        /// <example>[1,2,3]</example>
        public List<int> UserId { get; set; } = new List<int>();
    }
}