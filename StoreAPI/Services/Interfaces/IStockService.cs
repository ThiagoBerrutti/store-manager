using StoreAPI.Domain;
using StoreAPI.Dtos;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IStockService
    {
        public Task<ServiceResponse<PaginatedList<ProductStockReadDto>>> GetAllDtoPaginatedAsync(StockParametersDto parameters);

        public Task<ServiceResponse<ProductStockReadDto>> GetDtoByProductIdAsync(int productId);

        public Task<ServiceResponse<ProductStockReadDto>> GetDtoByIdAsync(int id);

        public ProductStock CreateProductStock(Product product, int startingQuantity = 0);

        public Task<ServiceResponse<ProductStockReadDto>> UpdateAsync(int id, ProductStockWriteDto dto);

        public Task<ServiceResponse<ProductStockReadDto>> RemoveProductQuantityAsync(int id, int quantity);

        public Task<ServiceResponse<ProductStockReadDto>> AddProductQuantityAsync(int id, int quantity);
    }
}