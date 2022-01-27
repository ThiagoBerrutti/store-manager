using SalesAPI.Models;
using SalesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IStockService
    {
        public Task CreateProductStock(int productId, int quantity);
        public void CreateProductStock(Product product, int startingAmount = 0);


        public Task<StockReadDto> GetByProductId(int productId);
        public Task<IEnumerable<StockReadDto>> GetAllAsync();

        public Task Update(int productId, StockWriteDto dto);

        public Task Delete(int id);
        public Task Clear();

        public Task RemoveProductAmount(int productId, int quantity);
        public Task AddProductAmount(int productId, int quantity);




    }
}
