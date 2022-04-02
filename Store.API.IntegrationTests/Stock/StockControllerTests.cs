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
    public class StockControllerTests : TestBase, IAsyncLifetime
    {
        public StockControllerTests(TestWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllStocksPaginated_ReturnAllFiveStocks_WhenServerIsNew()
        {
            // Arrange
            var uri = ApiRoutes.Stock.GetAllStocksPaginated;

            await Helpers.AuthenticateAsAdminAsync();
            var stocksOnDb = await Helpers.Stock.GetProductStocksAsync();

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


        [Fact]
        public async Task GetStockByProductId_SuccessfullyReturnsStock()
        {
            // Arrange
            const int productId = 1;

            var route = ApiRoutes.Stock.GetStockByProductId;
            var uri = route.Replace("{productId}", productId.ToString());

            var productOnDb = await Helpers.Product.GetProductWithFKAsync(p => p.Id == productId);

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<ProductStockReadDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(productOnDb.ProductStock.Id, result.Id);
            Assert.Equal(productOnDb.ProductStock.Quantity, result.Quantity);
            Assert.Equal(productOnDb.Id, result.ProductId);
            Assert.Equal(productOnDb.Name, result.ProductName);
        }


        [Fact]
        public async Task GetStockById_SuccessfullyReturnsProductStock()
        {
            // Arrange
            var productCreated = await Helpers.Product.CreateNewProductAsync();
            var id = productCreated.ProductStock.Id;
            var route = ApiRoutes.Stock.GetStockById;
            var uri = route.Replace("{id}", id.ToString());

            //var productStockOnDb = await Context.ProductStocks
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(ps => ps.Id == id);
            var productStockOnDb = await Helpers.Stock.GetProductStocksAsync();

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<ProductStockReadDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(productStockOnDb);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }


        [Fact]
        public async Task UpdateStock_SuccessfullyUpdates()
        {
            // Arrange
            var productCreated = await Helpers.Product.CreateNewProductAsync();
            
            var id = productCreated.Id;
            var productStockOriginal = await Context.ProductStocks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ps => ps.Id == id);

            var updatedQuantity = new Random().Next(productStockOriginal.Quantity + 1, (productStockOriginal.Quantity + 1) * 100);
            var stockUpdate = new ProductStockWriteDto { Quantity = updatedQuantity };

            var route = ApiRoutes.Stock.UpdateStock;
            var uri = route.Replace("{id}", id.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PutAsJsonAsync(uri, stockUpdate);
            var result = await response.Content.ReadAsAsync<ProductStockReadDto>();

            var productStockUpdated = await Context.ProductStocks
                .AsNoTracking()
                .FirstOrDefaultAsync(ps => ps.Id == id);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);

            Assert.Equal(productStockOriginal.Id, result.Id);
            Assert.NotEqual(productStockOriginal.Quantity, result.Quantity);

            Assert.NotEqual(productStockOriginal.Quantity, productStockUpdated.Quantity);
        }

        [Fact]
        public async Task AddQuantityToStock_SuccessfullyAddsQuantity()
        {
            // Arrange
            var product = await Helpers.Product.CreateNewProductAsync();
            var id = product.Id;
            var productStock = await Helpers.Stock.GetProductStockAsync(ps => ps.Id == id);

            const int QUANTITY = 1000;

            var route = ApiRoutes.Stock.AddQuantityToStock;
            var uri = route.Replace("{id}", id.ToString()).Replace("{quantity}", QUANTITY.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PutAsync(uri, null);
            var result = await response.Content.ReadAsAsync<ProductStockReadDto>();

            var productStockOnDb = await Helpers.Stock.GetProductStockAsync(ps => ps.Id == id);

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(productStockOnDb);

            Assert.Equal(productStock.Quantity + QUANTITY, productStockOnDb.Quantity);
        }


        [Fact]
        public async Task RemoveQuantityFromStock_SuccessfullyRemovesQuantity()
        {
            // Arrange
            var product = await Helpers.Product.CreateNewProductAsync();
            var id = product.Id;
            var productStock = await Helpers.Stock.GetProductStockAsync(ps => ps.Id == id);

            const int QUANTITY = 1000;

            var route = ApiRoutes.Stock.RemoveQuantityFromStock;
            var uri = route.Replace("{id}", id.ToString()).Replace("{quantity}", QUANTITY.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PutAsync(uri, null);
            var result = await response.Content.ReadAsAsync<ProductStockReadDto>();

            var productStockOnDb = await Helpers.Stock.GetProductStockAsync(ps => ps.Id == id);

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(productStockOnDb);

            Assert.Equal(productStock.Quantity - QUANTITY, productStockOnDb.Quantity);
        }






        public async Task InitializeAsync()
        {
            await Context.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
        }
    }
}