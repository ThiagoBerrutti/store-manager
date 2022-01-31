using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IEmployeeService
    {
        public Task CreateAsync(EmployeeWriteDto dto);

        public Task<IEnumerable<EmployeeReadDto>> GetAllAsync();

        public Task<EmployeeReadDto> GetByIdAsync(int id);

        public Task<IEnumerable<EmployeeReadDto>> GetByNameAsync(string name);

        //public Task<IEnumerable<EmployeeReadDto>> GetByRoleId(int roleId);

        public Task UpdateAsync(int id, EmployeeWriteDto employeeUpdate);

        public Task DeleteAsync(int id);

        public Task<IEnumerable<EmployeeReadDto>> SearchAsync(string name = "");

    }
}