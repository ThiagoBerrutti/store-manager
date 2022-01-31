using System.Collections.Generic;

namespace SalesAPI.Dtos
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public List<RoleReadDto> Roles { get; set; }
    }
}