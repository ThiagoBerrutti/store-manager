using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IEmployeeRepository
    {
        public void CreateAsync(Employee employee);

        public Task<IEnumerable<Employee>> GetAllAsync();

        public Task<IEnumerable<Employee>> GetAllWhereAsync(Expression<Func<Employee, bool>> predicate);

        public Task<Employee> GetByIdAsync(int id);

        public void UpdateAsync(Employee employee);

        public void Delete(Employee employee);
    }
}