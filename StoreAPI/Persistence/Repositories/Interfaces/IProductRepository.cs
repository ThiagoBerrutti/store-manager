using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetByIdAsync(int id);

        public Task<IEnumerable<Product>> SearchAsync(string search);

        public void Add(Product product);

        public void Update(Product product);

        public void Delete(Product product);
    }
}