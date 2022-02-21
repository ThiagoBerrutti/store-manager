using Microsoft.AspNetCore.Identity;
using SalesAPI.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();

        public Task<User> GetByUserNameAsync(string userName);

        public Task<User> GetCurrentUserAsync();

        public Task<User> GetByIdAsync(int id);

        public Task<IEnumerable<User>> SearchAsync(string search);

        public Task<IList<string>> GetRolesNamesAsync(User user);

        public Task<IdentityResult> CreateAsync(User user, string password);

        public Task<IdentityResult> UpdateUserAsync(User user);

        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        public Task<IdentityResult> ResetPasswordAsync(User user, string newPassword);

        public Task<IdentityResult> AddToRoleAsync(User user, string roleName);

        public Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);

        public Task ResetUser(User user, string password, IEnumerable<string> rolesNames);
    }
}