using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Identity;
using SalesAPI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SalesDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(SalesDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
        }

        public async Task<User> GetCurrentUser()
        {
            var currentUserNameId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.Users.Include(u => u.Roles).FirstOrDefaultAsync(u=> u.Id.ToString() == currentUserNameId);

            return user;

        }
    }
}