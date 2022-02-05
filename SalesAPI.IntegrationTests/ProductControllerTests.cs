using FluentAssertions;
using SalesAPI.Dtos;
using SalesAPI.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SalesAPI.IntegrationTests
{
    public class ProductControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllProducts_WithoutAnyProduct_ReturnsEmptyList()
        {
            // Arrange
            await RegisterAsync("admin");

            // Assert
            var response = await TestClient.GetAsync(ApiRoutes.Product.GetAllProducts);

            // Act
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsJsonAsync<List<ProductReadDto>>()).Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllProducts_WithoutAnyProduct_ReturnsEmptyLi2st()
        {
            // Arrange
            await AuthenticateAsync("manager");

            // Assert
            var response = await TestClient.GetAsync(ApiRoutes.Product.GetAllProducts);

            // Act
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsJsonAsync<List<ProductReadDto>>()).Should().BeEmpty();
        }





    }
}