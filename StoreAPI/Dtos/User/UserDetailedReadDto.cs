using System;
using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class UserDetailedReadDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<RoleReadDto> Roles { get; set; }
    }
}