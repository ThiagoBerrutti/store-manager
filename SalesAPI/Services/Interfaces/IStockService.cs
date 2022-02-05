using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IStockService
    {
        public Task<ProductStockReadDto> GetDtoByProductId(int productId);

        public Task<IEnumerable<ProductStockReadDto>> GetAllDtoAsync();

        public ProductStock CreateProductStock(Product product, int startingAmount = 0);

        public Task<ProductStockReadDto> Update(int productId, ProductStockWriteDto dto);

        public Task Delete(int id);

        public Task<ProductStockReadDto> RemoveProductAmount(int productId, int quantity);

        public Task<ProductStockReadDto> AddProductAmount(int productId, int quantity);
    }
}