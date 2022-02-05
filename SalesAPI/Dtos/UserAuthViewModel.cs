using System.Collections.Generic;

namespace SalesAPI.Dtos
{
    public class UserAuthViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public List<RoleReadDto> Roles { get; set; }
    }
}