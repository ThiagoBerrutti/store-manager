using StoreAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IProductService
    {
        public Task<ProductWithStockDto> CreateAsync(ProductWriteDto productDto, int amount = 0);

        public Task<IEnumerable<ProductReadDto>> GetAllDtoAsync();

        public Task<PagedList<ProductReadDto>> GetAllDtoPaginatedAsync(ProductParametersDto parameters);

        public Task<IEnumerable<ProductReadDto>> SearchDtosAsync(string search);

        public Task<ProductReadDto> GetDtoByIdAsync(int id);

        public Task<ProductReadDto> UpdateAsync(int productId, ProductWriteDto product);

        public Task DeleteAsync(int id);
    }
}