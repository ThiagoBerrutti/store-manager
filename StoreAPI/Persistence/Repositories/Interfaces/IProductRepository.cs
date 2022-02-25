using StoreAPI.Domain;
using StoreAPI.Dtos;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetAllWherePagedAsync(int pageNumber, int pageSize, Expression<Func<Product, bool>> expression);

        public Task<Product> GetByIdAsync(int id);

        public void Add(Product product);

        public void Update(Product product);

        public void Delete(Product product);
    }
}