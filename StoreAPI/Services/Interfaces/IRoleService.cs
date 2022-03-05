using StoreAPI.Dtos;
using StoreAPI.Identity;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IRoleService
    {
        public Task<PaginatedList<RoleReadDto>> GetAllDtoPaginatedAsync(RoleParametersDto parameters);

        public Task<Role> GetByIdAsync(int id);

        public Task<RoleReadDto> GetDtoByIdAsync(int id);

        public Task<Role> GetByNameAsync(string name);

        public Task<RoleReadDto> GetDtoByNameAsync(string name);

        public Task<PaginatedList<UserReadDto>> GetAllUsersOnRolePaginatedAsync(int id, QueryStringParameterDto parameters);

        public Task<RoleReadDto> CreateAsync(RoleWriteDto role);

        public Task DeleteAsync(int id);
    }
}