using StoreAPI.Dtos;
using StoreAPI.Identity;
using StoreAPI.Services.Communication;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IRoleService
    {
        public Task<ServiceResponse<PaginatedList<RoleReadDto>>> GetAllDtoPaginatedAsync(RoleParametersDto parameters);

        public Task<ServiceResponse<Role>> GetByIdAsync(int id);

        public Task<ServiceResponse<RoleReadDto>> GetDtoByIdAsync(int id);

        public Task<ServiceResponse<Role>> GetByNameAsync(string name);

        public Task<ServiceResponse<RoleReadDto>> GetDtoByNameAsync(string name);

        public Task<ServiceResponse<PaginatedList<UserReadDto>>> GetAllUsersOnRolePaginatedAsync(int id, QueryStringParameterDto parameters);

        public Task<ServiceResponse<RoleReadDto>> CreateAsync(RoleWriteDto role);

        public Task<ServiceResponse> DeleteAsync(int id);
    }
}