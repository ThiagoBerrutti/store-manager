﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreAPI;
using StoreAPI.Dtos;
using StoreAPI.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests.Products
{
    public class ProductControllerTests : TestBase, IDisposable
    {
        public ProductControllerTests(TestWebApplicationFactory<Startup> factory) : base(factory)
        {
            //Factory = factory;
        }

        private async Task RemoveAllProductsAsync()
        {
            foreach (var p in Context.Products)
            {
                Context.Products.Remove(p);
            }

            await Context.SaveChangesAsync();
        }

        private async Task<List<ProductReadWithStockDto>> CreateNewProduct(int quantity = 1)
        {
            var uri = ApiRoutes.Products.CreateProduct;
            await AuthenticateAsAdminAsync();

            var result = new List<ProductReadWithStockDto>();

            for (int i = 0; i < quantity; i++)
            {
                var productToCreate = ProductObjects.Factory.GenerateProductWriteDto();
                var response = await Client.PostAsJsonAsync(uri, productToCreate);
                var product = await response.Content.ReadAsAsync<ProductReadWithStockDto>();
                result.Add(product);
            }

            LogoutUser();

            return result;
        }



        [Fact]
        public async Task GetAllProductsPaginated_ReturnsAllThreeResults()
        {
            // Arrange
            const int PRODUCT_COUNT = 5;
            await RemoveAllProductsAsync();
            var productsList = await CreateNewProduct(PRODUCT_COUNT);

            await AuthenticateAsAdminAsync();

            var uri = ApiRoutes.Products.GetAllProductsPaginated;

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<List<ProductReadDto>>();

            var productsListNames = productsList.Select(pc => pc.Name);
            var productsCreatedContainedOnResult = result
                .Where(p => productsListNames.Contains(p.Name))
                .ToList();
            
            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(PRODUCT_COUNT, result.Count);
            Assert.Equal(productsList.Count, productsCreatedContainedOnResult.Count);
        }


        [Fact]
        public async Task GetAllProducts_ReturnEmptyList_WhenNoProducts()
        {
            // Arrange
            await AuthenticateAsAdminAsync();
            await RemoveAllProductsAsync();
            var uri = ApiRoutes.Products.GetAllProductsPaginated;

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<List<ProductReadDto>>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task GetProductById_Returns_Product()
        {
            // Arrange

            var products = await CreateNewProduct();
            var productCreated = products.Single();
            var productId = productCreated.Id;
            var uri = ApiRoutes.Products.GetProductById.Replace("{id}", productId.ToString());

            await AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var productReadDto = await response.Content.ReadFromJsonAsync<ProductReadDto>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(productReadDto);
            Assert.IsType<ProductReadDto>(productReadDto);

            Assert.Equal(productId, productReadDto.Id);
            Assert.NotEmpty(productReadDto.Name);
            Assert.Equal(productCreated.Name, productReadDto.Name);
        }

        [Fact]
        public async Task CreateProduct_ReturnsProductCreated()
        {
            // Arrange
            await AuthenticateAsAdminAsync();
            var product = ProductObjects.Factory.GenerateProductWriteDto();
            var uri = ApiRoutes.Products.CreateProduct;

            // Act
            var response = await Client.PostAsJsonAsync(uri, product);
            var result = await response.Content.ReadAsAsync<ProductReadWithStockDto>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
        }


        [Fact]
        public async Task CreateProduct_CreatesNewProductStock_WhenSuccessfull()
        {
            // Arrange
            var uri = ApiRoutes.Products.CreateProduct;
            var productToCreate = ProductObjects.Factory.GenerateProductWriteDto();

            await AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PostAsJsonAsync(uri, productToCreate);
            var result = await response.Content.ReadAsAsync<ProductReadWithStockDto>();

            var productStock = await Context.ProductStocks.FirstOrDefaultAsync(ps => ps.Product.Id == result.Id);

            // Assert 
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(productStock);
        }

        [Fact]
        public async Task DeleteProduct_SuccessfullyRemovesProduct()
        {
            // Arrange
            var product = (await CreateNewProduct()).FirstOrDefault();

            var productWasCreated = product != null;
            var uri = ApiRoutes.Products.DeleteProduct.Replace("{id}", product.Id.ToString());

            await AuthenticateAsAdminAsync();

            // Act
            var response = await Client.DeleteAsync(uri);
            var productOnDb = await Context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            // Assert
            Assert.True(productWasCreated);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Null(productOnDb);
        }

        [Fact]
        public async Task DeleteProduct_Returns404_WhenProductDoesntExist()
        {
            // Arrange
            await AuthenticateAsAdminAsync();
            var uri = ApiRoutes.Products.DeleteProduct.Replace("{id}", "9999999");

            // Act
            var response = await Client.DeleteAsync(uri);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_SuccessfullyUpdates()
        {
            // Arrange
            var productList = await CreateNewProduct();
            var productCreated = productList.Single();
            var id = productCreated.Id;

            var uri = ApiRoutes.Products.UpdateProduct.Replace("{id}", id.ToString());

            var productUpdate = new ProductWriteDto
            {
                Name = "updated",
                Description = "updated",
                Price = 123.45
            };

            await AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PutAsJsonAsync(uri, productUpdate);
            var updateResponse = await response.Content.ReadAsAsync<ProductReadDto>();

            var productOnDb = await Context.Products.FirstOrDefaultAsync(p => p.Id == id);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(updateResponse);
            Assert.NotNull(productOnDb);

            Assert.Equal(productOnDb.Id, id);
            Assert.Equal(productOnDb.Name, productUpdate.Name);
            Assert.Equal(productOnDb.Description, productUpdate.Description);
            Assert.Equal(productOnDb.Price, productUpdate.Price);

            Assert.NotEqual(productCreated.Name, productOnDb.Name);
            Assert.NotEqual(productCreated.Description, productOnDb.Description);
            Assert.NotEqual(productCreated.Price, productOnDb.Price);
        }

        public new void Dispose()
        {
            Factory.Cleanup();
        }
    }
}