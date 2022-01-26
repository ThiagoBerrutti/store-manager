using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Repositories
{
    public interface IStockRepository
    {
        public IAsyncEnumerable<ProductStock> GetAll();
        public Task<ProductStock> GetByIdAsync(int id);
        public Task<ProductStock> GetByProductAsync(Product product);

        public void Create(ProductStock productStock);
        public void Update(ProductStock productStock);
        public void Delete(ProductStock productStock);
    }
}
