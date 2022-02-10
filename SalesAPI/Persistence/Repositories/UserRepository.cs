using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.Include(u => u.Roles).ToListAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users
                            .Include(u => u.Roles)
                            .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }


        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _context.Users
                            .Include(u => u.Roles)
                            .FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());
        }


        public async Task<IEnumerable<User>> SearchAsync(string search)
        {
            var result = await _context.Users
                                            .Where(u =>
                                                u.FirstName.ToLower().Contains(search.ToLower()) ||
                                                u.UserName.ToLower().Contains(search.ToLower()) ||
                                                u.LastName.ToLower().Contains(search.ToLower()))
                                            .ToListAsync();

            return result;
        }


        public async Task<User> GetCurrentUserAsync()
        {
            var currentUserNameId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.Users
                                .Include(u => u.Roles)
                                .FirstOrDefaultAsync(u => u.Id.ToString() == currentUserNameId);
            return user;
        }
        

        public async Task<IList<string>> GetRolesNamesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
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
            if (result.Succeeded)
            {
                var role = user.Roles.FirstOrDefault(r => r.NormalizedName == roleName.ToUpper());
                user.Roles.Remove(role);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}