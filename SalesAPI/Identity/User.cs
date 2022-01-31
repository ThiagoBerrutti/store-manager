using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SalesAPI.Models
{
    public class User : IdentityUser<int>
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}