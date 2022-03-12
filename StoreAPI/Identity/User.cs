using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace StoreAPI.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}