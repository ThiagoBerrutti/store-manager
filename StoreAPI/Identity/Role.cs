using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StoreAPI.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<User> Users { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}