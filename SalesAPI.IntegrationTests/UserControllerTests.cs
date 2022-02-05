using FluentAssertions;
using SalesAPI.Dtos;
using SalesAPI.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SalesAPI.IntegrationTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllUsers()
        {
            // Arrange
            await RegisterAsync("register");

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.User.GetAllUsers);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsJsonAsync<List<UserViewModel>>();
            content.Should().BeEmpty();


        }
    }
}