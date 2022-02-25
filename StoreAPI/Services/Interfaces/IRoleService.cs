using StoreAPI.Dtos;
using StoreAPI.Dtos.Shared;
using StoreAPI.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IRoleService
    {
        public Task<IEnumerable<RoleReadDto>> GetAllDtoAsync();

        public Task<PagedList<RoleReadDto>> GetAllPagedAsync(RoleParametersDto parameters);

        public Task<Role> GetByIdAsync(int id);

        public Task<RoleReadDto> GetDtoByIdAsync(int id);

        public Task<Role> GetByNameAsync(string name);

        public Task<RoleReadDto> GetDtoByNameAsync(string name);

        public Task<IEnumerable<UserReadDto>> GetAllUsersOnRole(int id);

        public Task<RoleReadDto> CreateAsync(RoleWriteDto role);

        public Task DeleteAsync(int id);
    }
}