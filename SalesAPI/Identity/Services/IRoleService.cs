using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public interface IRoleService
    {
        public Task<RoleReadDto> CreateAsync(RoleWriteDto role);

        public Task DeleteAsync(int id);

        public Task<IEnumerable<RoleReadDto>> GetAllAsync();

        public Task<RoleReadDto> GetByIdAsync(int id);

        public Task<RoleReadDto> GetByName(string name);
    }
}