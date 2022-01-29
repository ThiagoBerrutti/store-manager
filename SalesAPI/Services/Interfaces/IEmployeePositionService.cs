using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IEmployeePositionService
    {
        public Task<EmployeePosition> CreateAsync(EmployeePositionWriteDto employeePosition);

        public Task DeleteAsync(int id);

        public Task<IEnumerable<EmployeePosition>> GetAllAsync();

        public Task<EmployeePosition> GetByIdAsync(int id);

        public Task<IEnumerable<EmployeePosition>> GetByName(string name);

        public Task UpdateNameAsync(int id, string name);
    }
}