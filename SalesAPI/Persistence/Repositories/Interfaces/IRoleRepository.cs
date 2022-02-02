using Microsoft.AspNetCore.Identity;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAllAsync();
        public Task<Role> GetByNameAsync(string roleName);
        public Task<Role> GetByIdAsync(int id);
        public Task<IEnumerable<Role>> SearchByNameAsync(string name);
        public Task<IdentityResult> CreateAsync(Role role);
        public Task<IdentityResult> DeleteAsync(Role role);

        //public Task<IdentityResult> AddUserToRoleAsync(User user, string roleName);

        //public Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);

    }
}