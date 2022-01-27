using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetByIdAsync(int id);

        public void Add(Product product);

        public void Update(Product product);

        public void Delete(Product product);
    }
}