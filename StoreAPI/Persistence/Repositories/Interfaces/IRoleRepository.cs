using Microsoft.AspNetCore.Identity;
using StoreAPI.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAllAsync();

        public Task<Role> GetByNameAsync(string roleName);

        public Task<Role> GetByIdAsync(int id);

        public Task<IEnumerable<Role>> SearchByNameAsync(string name);

        public Task<IdentityResult> CreateAsync(Role role);

        public Task<IdentityResult> DeleteAsync(Role role);
    }
}