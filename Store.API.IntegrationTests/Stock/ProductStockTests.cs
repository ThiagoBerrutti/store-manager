using Microsoft.EntityFrameworkCore;
using StoreAPI;
using StoreAPI.Dtos;
using StoreAPI.Infra;
using StoreAPI.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests.Stock
{
    public class ProductStockTests : TestBase, IDisposable
    {
        public ProductStockTests(TestWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllStocksPaginated_ReturnAllFiveStocks_WhenServerIsNew()
        {
            // Arrange
            var uri = ApiRoutes.Stock.GetAllStocksPaginated;

            await AuthenticateAsAdminAsync();
            var stocksOnDb = await Context.ProductStocks.ToListAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<List<ProductStockReadDto>>();

            // Assert 
            var stocksOnDbIds = stocksOnDb.Select(s => s.Id);
            var resultIds = result.Select(s => s.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(stocksOnDbIds, resultIds);
            Assert.Equal(SeedData.ProductStocks.Count, result.Count);
        }

        public new void Dispose()
        {
            Factory.Cleanup();
        }
    }
}