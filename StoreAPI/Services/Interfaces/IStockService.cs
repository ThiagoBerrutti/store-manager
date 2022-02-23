using StoreAPI.Dtos;
using StoreAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IStockService
    {
        public Task<IEnumerable<ProductStockReadDto>> GetAllDtoAsync();

        public Task<ProductStockReadDto> GetDtoByProductIdAsync(int productId);

        public Task<ProductStockReadDto> GetDtoByIdAsync(int id);

        public ProductStock CreateProductStock(Product product, int startingAmount = 0);

        public Task<ProductStockReadDto> UpdateAsync(int id, ProductStockWriteDto dto);

        public Task<ProductStockReadDto> RemoveProductAmountAsync(int id, int amount);

        public Task<ProductStockReadDto> AddProductAmountAsync(int id, int amount);
    }
}