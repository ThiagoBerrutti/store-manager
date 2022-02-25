using StoreAPI.Domain;
using StoreAPI.Dtos;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IStockRepository
    {
        public Task<PagedList<ProductStock>> GetAllWherePagedAsync(int pageNumber, int pageSize, Expression<Func<ProductStock, bool>> expression);

        public Task<ProductStock> GetByProductIdAsync(int productId);

        public Task<ProductStock> GetByIdAsync(int id);

        public void Create(ProductStock productStock);

        public void Update(ProductStock productStock);

        public void Delete(ProductStock productStock);
    }
}