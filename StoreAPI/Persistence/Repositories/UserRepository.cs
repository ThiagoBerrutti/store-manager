using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Domain;
using StoreAPI.Dtos;
using StoreAPI.Extensions;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(StoreDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        

        public async Task<PaginatedList<User>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<User, bool>> expression)
        {
            var result = await _context.Users
                    .OrderBy(u => u.FirstName)
                    .ThenBy(p => p.Id)
                    .Where(expression)
                    .Include(u => u.Roles)
                    .ToPaginatedListAsync(pageNumber, pageSize);

            return result;
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


        public async Task<List<User>> GetUserByNameRangeAsync(IEnumerable<string> userNames)
        {
            var result = await _context.Users.Where(u => userNames.Contains(u.UserName)).ToListAsync();
            return result;
        }


        public async Task ResetTestUsers()
        {
            var hasher = new PasswordHasher<User>();

            var userNames = new List<string> {
                AppConstants.Users.Admin.UserName,
                AppConstants.Users.Manager.UserName,
                AppConstants.Users.Stock.UserName,
                AppConstants.Users.Seller.UserName,
                AppConstants.Users.Public.UserName 
            };

            var users = await GetUserByNameRangeAsync(userNames);

            var adminUser = users.FirstOrDefault(u => u.UserName == AppConstants.Users.Admin.UserName);
            var managerUser = users.FirstOrDefault(u => u.UserName == AppConstants.Users.Manager.UserName);
            var stockUser = users.FirstOrDefault(u => u.UserName == AppConstants.Users.Stock.UserName);
            var sellerUser = users.FirstOrDefault(u => u.UserName == AppConstants.Users.Seller.UserName);
            var publicUser = users.FirstOrDefault(u => u.UserName == AppConstants.Users.Public.UserName);

            var roles = await _context.Roles.Where(r =>
                r.Name == AppConstants.Roles.Admin.Name ||
                r.Name == AppConstants.Roles.Manager.Name ||
                r.Name == AppConstants.Roles.Stock.Name ||
                r.Name == AppConstants.Roles.Seller.Name).ToListAsync();

            var adminRoles = new List<Role> { roles.FirstOrDefault(r => r.Name == AppConstants.Roles.Admin.Name) };
            var managerRoles = new List<Role> { roles.FirstOrDefault(r => r.Name == AppConstants.Roles.Manager.Name) };
            var stockRoles = new List<Role> { roles.FirstOrDefault(r => r.Name == AppConstants.Roles.Stock.Name) };
            var sellerRoles = new List<Role> { roles.FirstOrDefault(r => r.Name == AppConstants.Roles.Seller.Name) };

            adminUser.FirstName = AppConstants.Users.Admin.FirstName;
            adminUser.LastName = AppConstants.Users.Admin.LastName;
            adminUser.PasswordHash = hasher.HashPassword(adminUser, AppConstants.Users.Admin.Password);
            adminUser.DateOfBirth = DateTime.Parse(AppConstants.Users.Admin.DateOfBirth);
            await SetUserRoles(adminUser, adminRoles);

            managerUser.FirstName = AppConstants.Users.Manager.FirstName;
            managerUser.LastName = AppConstants.Users.Manager.LastName;
            managerUser.PasswordHash = hasher.HashPassword(managerUser, AppConstants.Users.Manager.Password);
            managerUser.DateOfBirth = DateTime.Parse(AppConstants.Users.Manager.DateOfBirth);
            await SetUserRoles(managerUser, managerRoles);

            stockUser.FirstName = AppConstants.Users.Stock.FirstName;
            stockUser.LastName = AppConstants.Users.Stock.LastName;
            stockUser.PasswordHash = hasher.HashPassword(stockUser, AppConstants.Users.Stock.Password);
            stockUser.DateOfBirth = DateTime.Parse(AppConstants.Users.Stock.DateOfBirth);
            await SetUserRoles(stockUser, stockRoles);

            sellerUser.FirstName = AppConstants.Users.Seller.FirstName;
            sellerUser.LastName = AppConstants.Users.Seller.LastName;
            sellerUser.PasswordHash = hasher.HashPassword(sellerUser, AppConstants.Users.Seller.Password);
            sellerUser.DateOfBirth = DateTime.Parse(AppConstants.Users.Seller.DateOfBirth);
            await SetUserRoles(sellerUser, sellerRoles);

            publicUser.FirstName = AppConstants.Users.Public.FirstName;
            publicUser.LastName = AppConstants.Users.Public.LastName;
            publicUser.PasswordHash = hasher.HashPassword(publicUser, AppConstants.Users.Public.Password);
            publicUser.DateOfBirth = DateTime.Parse(AppConstants.Users.Public.DateOfBirth);
            await SetUserRoles(publicUser, new List<Role>());
        }


        private async Task SetUserRoles(User user, List<Role> roles)
        {
            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();
            _context.UserRoles.RemoveRange(userRoles);

            foreach (var r in roles)
            {
                _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = r.Id });
            }

            user.Roles = roles;
        }
    }
}