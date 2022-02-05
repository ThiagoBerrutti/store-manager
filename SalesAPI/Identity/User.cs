using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SalesAPI.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfbirth { get; set; }

        public List<Role> Roles { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}