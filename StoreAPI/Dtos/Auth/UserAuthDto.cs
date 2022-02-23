using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class UserAuthDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public List<RoleReadDto> Roles { get; set; }
    }
}