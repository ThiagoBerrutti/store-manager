using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IProductStockRepository
    {
        public Task<IEnumerable<ProductStock>> GetAll();

        public Task<ProductStock> GetByIdAsync(int stockId);

        public Task<ProductStock> GetByProductIdAsync(int id);

        public Task<ProductStock> GetByProductAsync(Product product);

        public void Create(ProductStock productStock);

        public void Update(ProductStock productStock);

        public void Delete(ProductStock productStock);
    }
}