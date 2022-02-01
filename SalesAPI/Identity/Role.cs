using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SalesAPI.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}