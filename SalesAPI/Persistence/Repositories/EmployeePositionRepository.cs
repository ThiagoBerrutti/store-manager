//using Microsoft.EntityFrameworkCore;
//using SalesAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace SalesAPI.Persistence.Repositories
//{
//    public class RoleRepository : IRoleRepository
//    {
//        private readonly SalesDbContext _context;

//        public RoleRepository(SalesDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Role>> GetAllAsync()
//        {
//            return await _context.EmployeeRoles
//                .Include(p => p.Employees)
//                .ToListAsync();
//        }

//        public async Task<Role> GetByIdAsync(int id)
//        {
//            return await _context.EmployeeRoles
//                .Include(p => p.Employees)
//                .FirstOrDefaultAsync(e => e.Id == id);
//        }

//        public async Task<IEnumerable<Role>> GetAllWhereAsync(Expression<Func<Role, bool>> predicate)
//        {
//            return await _context.EmployeeRoles.Include(p => p.Employees).Where(predicate).ToListAsync(); 
//        }

//        public void Add(Role role)
//        {
//            _context.EmployeeRoles.Add(role);
//        }

//        public void UpdateAsync(Role role)
//        {
//            _context.EmployeeRoles.Update(role);
//        }

//        public void Delete(Role role)
//        {
//            _context.EmployeeRoles.Remove(role);
//        }
//    }
//}