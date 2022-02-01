using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IStockService
    {
        public ProductStock CreateProductStock(Product product, int startingAmount = 0);

        public Task<StockReadDto> GetByProductId(int productId);

        public Task<IEnumerable<StockReadDto>> GetAllAsync();

        public Task Update(int productId, StockWriteDto dto);

        public Task Delete(int id);

        public Task RemoveProductAmount(int productId, int quantity);

        public Task AddProductAmount(int productId, int quantity);
    }
}