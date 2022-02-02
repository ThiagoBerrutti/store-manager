using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IProductService
    {
        public Task CreateAsync(ProductWriteDto product);

        public Task<IEnumerable<ProductReadDto>> GetAllDtoAsync();

        public Task<ProductReadDto> GetDtoByIdAsync(int id);

        public Task<ProductReadDto> UpdateAsync(int productId, ProductWriteDto product);

        public Task DeleteAsync(int id);
    }
}