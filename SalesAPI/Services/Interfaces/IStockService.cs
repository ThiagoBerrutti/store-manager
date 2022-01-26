using SalesAPI.Models;
using SalesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IStockService
    {
        public Task AddProduct(int productId, int quantity);

        public Task RemoveProduct(int productId, int quantity);

        public Task<IAsyncEnumerable<ProductStock>> GetAllProducts();

    }
}
