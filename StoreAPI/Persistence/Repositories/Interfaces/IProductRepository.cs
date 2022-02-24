using StoreAPI.Domain;
using StoreAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<PagedList<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize);

        public Task<PagedList<Product>> GetAllWherePagedAsync(int pageNumber, int pageSize, Expression<Func<Product, bool>> expression);

        public Task<PagedList<Product>> GetAllWithParameters(int pageNumber, int pageSize, int minPrice, int maxPrice, string name, string description, bool onStock);

        public Task<Product> GetByIdAsync(int id);

        //public Task<IEnumerable<Product>> SearchAsync(string search);

        public void Add(Product product);

        public void Update(Product product);

        public void Delete(Product product);
    }
}