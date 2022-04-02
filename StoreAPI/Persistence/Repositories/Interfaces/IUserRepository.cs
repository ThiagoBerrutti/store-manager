using Microsoft.AspNetCore.Identity;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<PaginatedList<User>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<User, bool>> expression);

        public Task<User> GetByUserNameAsync(string userName);

        public Task<User> GetCurrentUserAsync();

        public Task<User> GetByIdAsync(int id);

        //public Task<IEnumerable<User>> SearchAsync(string search);

        public Task<IList<string>> GetRolesNamesAsync(User user);

        public Task<IdentityResult> CreateAsync(User user, string password);

        public Task<IdentityResult> UpdateUserAsync(User user);

        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        public Task<IdentityResult> ResetPasswordAsync(User user, string newPassword);

        public Task<IdentityResult> AddToRoleAsync(User user, string roleName);

        public Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);

        public Task ResetTestUsers();

        public Task<List<User>> GetUserByNameRangeAsync(IEnumerable<string> userNames);
    }
}