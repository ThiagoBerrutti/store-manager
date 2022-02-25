using StoreAPI.Dtos;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IProductService
    {
        public Task<ProductWithStockDto> CreateAsync(ProductWriteDto productDto, int amount = 0);

        public Task<PagedList<ProductReadDto>> GetAllDtoPagedAsync(ProductParametersDto parameters);

        public Task<ProductReadDto> GetDtoByIdAsync(int id);

        public Task<ProductReadDto> UpdateAsync(int productId, ProductWriteDto product);

        public Task DeleteAsync(int id);
    }
}