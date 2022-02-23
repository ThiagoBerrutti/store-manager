using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public List<RoleReadDto> Roles { get; set; }
    }
}