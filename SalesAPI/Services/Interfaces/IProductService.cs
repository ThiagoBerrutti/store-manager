using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IProductService
    {
        public Task CreateAsync(ProductWriteDto product);

        public Task<IEnumerable<ProductReadDto>> GetAllAsync();
        public Task<ProductReadDto> GetByIdAsync(int id);

        public Task UpdateAsync(int productId, ProductWriteDto product);

        public Task DeleteAsync(int id);
        public Task Clear();


    }
}