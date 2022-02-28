using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Dtos;
using StoreAPI.Extensions;
using StoreAPI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly StoreDbContext _context;

        public RoleRepository(StoreDbContext context, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
        }



        public async Task<PaginatedList<Role>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<Role, bool>> expression)
        {
            var result = await _context.Roles
                    .OrderBy(r => r.Id)
                    .Include(r => r.Users)
                    .Where(expression)
                    .ToPaginatedListAsync(pageNumber, pageSize);                

            return result;
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