using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IProductService
    {
        public Task<ProductReadDto> CreateAsync(ProductWriteDto product);

        public Task<IEnumerable<ProductReadDto>> GetAllDtoAsync();

        public Task<IEnumerable<ProductReadDto>> SearchDtosAsync(string search);

        public Task<ProductReadDto> GetDtoByIdAsync(int id);

        public Task<ProductReadDto> UpdateAsync(int productId, ProductWriteDto product);

        public Task DeleteAsync(int id);
    }
}