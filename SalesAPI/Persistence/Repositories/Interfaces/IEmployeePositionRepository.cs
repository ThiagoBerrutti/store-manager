using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IEmployeePositionRepository
    {
        public Task<IEnumerable<EmployeePosition>> GetAllAsync();

        public Task<EmployeePosition> GetByIdAsync(int id);

        public Task<IEnumerable<EmployeePosition>> GetAllWhereAsync(Expression<Func<EmployeePosition, bool>> predicate);

        public void Add(EmployeePosition employeePosition);

        public void UpdateAsync(EmployeePosition employeePosition);

        public void Delete(EmployeePosition employeePosition);
    }
}