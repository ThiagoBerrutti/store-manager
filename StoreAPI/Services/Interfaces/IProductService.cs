using StoreAPI.Dtos;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IProductService
    {
        public Task<ServiceResponse<ProductReadWithStockDto>> CreateAsync(ProductWriteDto productDto, int quantity = 0);

        public Task<ServiceResponse<PaginatedList<ProductReadDto>>> GetAllDtoPaginatedAsync(ProductParametersDto parameters);

        public Task<ServiceResponse<ProductReadDto>> GetDtoByIdAsync(int id);

        public Task<ServiceResponse<ProductReadDto>> UpdateAsync(int productId, ProductWriteDto product);

        public Task<ServiceResponse<ProductReadDto>> DeleteAsync(int id);
    }
}