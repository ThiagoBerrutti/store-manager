using Store.API.IntegrationTests.Auth;
using Store.API.IntegrationTests.Products;
using Store.API.IntegrationTests.Roles;
using Store.API.IntegrationTests.Stock;
using Store.API.IntegrationTests.Users;
using StoreAPI.Dtos;
using StoreAPI.Infra;
using StoreAPI.Persistence;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Store.API.IntegrationTests
{
    public class TestHelpers
    {
        public HttpClient Client { get; }
        public StoreDbContext Context { get; }
        public ProductTestHelpers Product { get; }
        public ProductStockTestHelpers Stock { get; }
        public RoleTestHelpers Role { get; }
        public UserTestHelpers User { get; }

        public TestHelpers(HttpClient client, StoreDbContext context)
        {
            Client = client;
            Context = context;
            Product = new ProductTestHelpers(client, context);
            Stock = new ProductStockTestHelpers(client, context);
            Role = new RoleTestHelpers(client, context);
            User = new UserTestHelpers(client, context);
        }

        public async Task AuthenticateAsync(string userName, string password)
        {
            var userLogin = new UserLoginDto { Password = password, UserName = userName };

            await AuthenticateAsync(userLogin);
        }


        public async Task AuthenticateAsync(UserLoginDto user)
        {
            var token = await GetJwtAsync(user);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }


        public async Task AuthenticateAsAdminAsync()
        {
            await AuthenticateAsync(AuthObjects.UserLogins.Admin);
        }


        public void LogoutUser()
        {
            Client.DefaultRequestHeaders.Authorization = null;
        }


        private async Task<string> GetJwtAsync(UserLoginDto user)
        {
            var uri = ApiRoutes.Auth.Authenticate;

            var responseMessage = await Client.PostAsJsonAsync(uri, user);

            var authResponse = await responseMessage.Content.ReadAsAsync<AuthResponse>();
            var token = authResponse.Token;

            return token;
        }
    }
}