using Microsoft.AspNetCore.Identity;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Identity
{
    public class User : IdentityUser<int>
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}