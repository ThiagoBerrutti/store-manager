using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }



        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var roles = await _roleManager.Roles
                                .Include(r => r.Users)
                                .ToListAsync();
            return roles;
        }


        public async Task<Role> GetByNameAsync(string roleName)
        {
            var role = await _roleManager.Roles
                                .Include(r => r.Users)
                                .FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpper());
            return role;
        }


        public async Task<Role> GetByIdAsync(int id)
        {
            var role = await _roleManager.Roles
                                .Include(r => r.Users)
                                .FirstOrDefaultAsync(r => r.Id == id);

            return role;
        }


        public async Task<IEnumerable<Role>> SearchByNameAsync(string name)
        {
            var roles = await _roleManager.Roles
                .Where(r => r.NormalizedName.Contains(name.ToUpper()))
                .Include(r => r.Users)
                .ToListAsync();

            return roles;
        }


        public async Task<IdentityResult> CreateAsync(Role role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }


        public async Task<IdentityResult> DeleteAsync(Role role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }
    }
}