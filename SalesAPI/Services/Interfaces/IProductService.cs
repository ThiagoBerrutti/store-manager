using SalesAPI.Models;
using SalesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IProductService
    {
        public Task CreateProductAsync(ProductWriteDto product);
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product> GetAsync(Product product);
        public Task<Product> GetByIdAsync(int id);
        public Task UpdateAsync(ProductWriteDto product);
        public Task DeleteAsync(Product product);
    }
}
