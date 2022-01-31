//using SalesAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace SalesAPI.Persistence.Repositories
//{
//    public interface IRoleRepository
//    {
//        public Task<IEnumerable<Role>> GetAllAsync();

//        public Task<Role> GetByIdAsync(int id);

//        public Task<IEnumerable<Role>> GetAllWhereAsync(Expression<Func<Role, bool>> predicate);

//        public void Add(Role role);

//        public void UpdateAsync(Role role);

//        public void Delete(Role role);
//    }
//}