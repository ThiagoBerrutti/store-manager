using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StoreAPI.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}