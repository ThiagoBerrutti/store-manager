using Microsoft.AspNetCore.Identity;
using SalesAPI.Models;

namespace SalesAPI.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}