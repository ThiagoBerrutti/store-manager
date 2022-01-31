using Microsoft.AspNetCore.Identity;

namespace SalesAPI.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}