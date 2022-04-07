using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI;
using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Infra;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests.Auth
{
    public class AuthControllerTests : TestBase, IAsyncLifetime
    {
        public AuthControllerTests(TestWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Authenticate_SuccessfullyAuthenticatesAdministrator()
        {
            // Arrange
            var uri = ApiRoutes.Auth.Authenticate;

            // Act

            var responseMessage = await Client.PostAsJsonAsync(uri, AuthObjects.UserLogins.Admin);

            var authResponse = await responseMessage.Content.ReadAsAsync<AuthResponse>();

            var responseUser = authResponse.User;
            var responseToken = authResponse.Token;

            // Assert

            Assert.True(responseMessage.IsSuccessStatusCode);
            Assert.NotNull(authResponse);
            Assert.Collection(responseUser.Roles, r => Assert.Equal(AppConstants.Roles.Admin.Name, r.Name));
            Assert.Equal(AppConstants.Users.Admin.UserName, responseUser.UserName);
            Assert.Equal(AppConstants.Users.Admin.Id, responseUser.Id);
            Assert.True(!string.IsNullOrEmpty(responseToken));
        }


        [Fact]
        public async Task Register_SuccessfullyRegistersNewUser()
        {
            // Arrange

            var uri = ApiRoutes.Auth.Register;
            var testUser = AuthObjects.Factories.UserRegisterDto();

            // Act

            var response = await Client.PostAsJsonAsync(uri, testUser);
            var registerResponse = await response.Content.ReadAsAsync<AuthResponse>();

            var userOnDb = await Context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == testUser.UserName);

            // Assert

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(userOnDb);
            Assert.Equal(testUser.FirstName, userOnDb.FirstName);
        }


        [Fact]
        public async Task Register_ReturnsCorrectAuthResponse_WhenSuccessful()
        {
            // Arrange
            var uri = ApiRoutes.Auth.Register;
            var testUser = AuthObjects.Factories.UserRegisterDto();

            // Act

            var response = await Client.PostAsJsonAsync(uri, testUser);
            var registerResponse = await response.Content.ReadAsAsync<AuthResponse>();

            var registeredUser = registerResponse?.User;
            var token = registerResponse?.Token;

            // Assert

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(registerResponse);
            Assert.True(!string.IsNullOrEmpty(token));
            Assert.Equal(testUser.UserName, registeredUser.UserName);
        }


        [Fact]
        public async Task RegisterTestAcc_SuccessfullyRegisters_RandomUser()
        {
            // Arrange
            var uri = ApiRoutes.Auth.RegisterTestAcc;

            // Act
            var response = await Client.PostAsync(uri, null);

            var authResponse = await response.Content.ReadAsAsync<AuthResponse>();
            var responseUser = authResponse.User;

            var userOnDb = await Context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == responseUser.UserName);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            Assert.NotNull(userOnDb);
            Assert.Contains(responseUser.UserName, userOnDb.UserName);
        }


        [Theory]
        [InlineData(RolesEnum.Administrator, RolesEnum.Manager, RolesEnum.Stock)]
        [InlineData(RolesEnum.Administrator, RolesEnum.Stock)]
        [InlineData(RolesEnum.Seller)]
        [InlineData(RolesEnum.Stock, RolesEnum.Seller, RolesEnum.Manager)]
        public async Task RegisterTestAcc_RegistersWithAssignedRoles(params RolesEnum[] roles)
        {
            // Arrange
            var uri = ApiRoutes.Auth.RegisterTestAcc;

            for (int i = 0; i < roles.Length; i++)
            {
                if (i == 0)
                {
                    uri += "?";
                }
                var r = roles[i];
                uri += $"roleId={r}";

                if (i + 1 < roles.Length)
                {
                    uri += "&";
                }
            }

            // Act
            var response = await Client.PostAsync(uri, null);

            var authResponse = await response.Content.ReadAsAsync<AuthResponse>();
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>();
            var responseUser = authResponse.User;

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            var userOnDb = await Context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == responseUser.UserName);
            Assert.NotNull(userOnDb);
            Assert.Contains(responseUser.UserName, userOnDb.UserName);
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