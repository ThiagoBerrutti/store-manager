using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(SalesDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.Include(u => u.Roles).ToListAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _userManager.Users
                                .Include(u => u.Roles)
                                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<User> GetByNameAsync(string userName)
        {
            return await _userManager.Users
                            .Include(u => u.Roles)
                            .FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
        }


        public async Task<User> GetCurrentUserAsync()
        {
            var currentUserNameId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.Users
                                .Include(u => u.Roles)
                                .FirstOrDefaultAsync(u => u.Id.ToString() == currentUserNameId);
            return user;
        }


        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

       
        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }


        public async Task<IdentityResult> ResetPasswordAsync(User user, string newPassword)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            
            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result;
        }
    }
}