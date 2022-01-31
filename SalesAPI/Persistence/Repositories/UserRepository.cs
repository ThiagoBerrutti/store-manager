using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
        }
    }
}