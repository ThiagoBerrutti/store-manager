using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class EmployeePositionRepository : IEmployeePositionRepository
    {
        private readonly SalesDbContext _context;

        public EmployeePositionRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeePosition>> GetAllAsync()
        {
            return await _context.EmployeePositions
                .Include(p => p.Employees)
                .ToListAsync();
        }

        public async Task<EmployeePosition> GetByIdAsync(int id)
        {
            return await _context.EmployeePositions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<EmployeePosition>> GetAllWhereAsync(Expression<Func<EmployeePosition, bool>> predicate)
        {
            return await _context.EmployeePositions.Include(p => p.Employees).Where(predicate).ToListAsync(); 
        }

        public void Add(EmployeePosition employeePosition)
        {
            _context.EmployeePositions.Add(employeePosition);
        }

        public void UpdateAsync(EmployeePosition employeePosition)
        {
            _context.EmployeePositions.Update(employeePosition);
        }

        public void Delete(EmployeePosition employeePosition)
        {
            _context.EmployeePositions.Remove(employeePosition);
        }
    }
}