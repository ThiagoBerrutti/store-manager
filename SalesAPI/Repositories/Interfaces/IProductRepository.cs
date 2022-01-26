using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAll();
        public  Task<Product> GetByIdAsync(int id);
        public void Add(Product product);
        public void Update(Product product);
        public void Delete(Product product);        
    }
}
