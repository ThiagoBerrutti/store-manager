using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class EmployeeRepository :IEmployeeRepository
    {
        private readonly SalesDbContext _context;

        public EmployeeRepository(SalesDbContext context)
        {
            _context = context;
        }

        public void CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Position)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllWhereAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _context.Employees
                .Include(e => e.Position)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _context.Remove(employee);
        }


    }
}
