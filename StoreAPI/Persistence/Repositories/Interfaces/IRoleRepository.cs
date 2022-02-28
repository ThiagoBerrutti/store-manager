using Microsoft.AspNetCore.Identity;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IRoleRepository
    {
        public Task<PaginatedList<Role>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<Role, bool>> expression);

        public Task<Role> GetByNameAsync(string roleName);

        public Task<Role> GetByIdAsync(int id);

        public Task<IdentityResult> CreateAsync(Role role);

        public Task<IdentityResult> DeleteAsync(Role role);
    }
}