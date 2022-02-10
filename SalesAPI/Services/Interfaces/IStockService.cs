using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IStockService
    {
        public Task<ProductStockReadDto> GetDtoByProductIdAsync(int productId);

        public Task<IEnumerable<ProductStockReadDto>> GetAllDtoAsync();

        public ProductStock CreateProductStock(Product product, int startingAmount = 0);

        public Task<ProductStockReadDto> UpdateAsync(int productId, ProductStockWriteDto dto);

        public Task DeleteAsync(int id);

        public Task<ProductStockReadDto> RemoveProductAmountAsync(int productId, int quantity);

        public Task<ProductStockReadDto> AddProductAmountAsync(int productId, int quantity);
    }
}