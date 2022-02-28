using StoreAPI.Domain;
using StoreAPI.Dtos;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IStockService
    {
        public Task<PaginatedList<ProductStockReadDto>> GetAllDtoPaginatedAsync(StockParametersDto parameters);

        public Task<ProductStockReadDto> GetDtoByProductIdAsync(int productId);

        public Task<ProductStockReadDto> GetDtoByIdAsync(int id);

        public ProductStock CreateProductStock(Product product, int startingQuantity = 0);

        public Task<ProductStockReadDto> UpdateAsync(int id, ProductStockWriteDto dto);

        public Task<ProductStockReadDto> RemoveProductQuantityAsync(int id, int quantity);

        public Task<ProductStockReadDto> AddProductQuantityAsync(int id, int quantity);
    }
}