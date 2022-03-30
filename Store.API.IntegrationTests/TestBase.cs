using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.API.IntegrationTests.Auth;
using StoreAPI;
using StoreAPI.Dtos;
using StoreAPI.Infra;
using StoreAPI.Persistence;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Store.API.IntegrationTests
{
    
    public class TestBase : IClassFixture<TestWebApplicationFactory<Startup>>, IDisposable
    {
        internal virtual ILogger Logger { get; set; }
        private readonly IServiceScope Scope;
        public TestWebApplicationFactory<Startup> Factory;
        public IServiceProvider ServiceProvider;
        public HttpClient Client;
        public StoreDbContext Context;
        public object Lock;

        public TestBase(TestWebApplicationFactory<Startup> factory) 
        {
            Factory = factory;
            Lock = factory.Lock;            
            Client = Factory.CreateClient();
            Scope = Factory.Services.CreateScope();
            ServiceProvider = Scope.ServiceProvider;            
            
            Context = ServiceProvider.GetRequiredService<StoreDbContext>();           
        }

        protected async Task AuthenticateAsync(UserLoginDto user)
        {
            var token = await GetJwtAsync(user);
            
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }


        protected async Task AuthenticateAsAdminAsync()
        {            
            await AuthenticateAsync (AuthObjects.UserLogins.Admin);
        }


        protected void LogoutUser()
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

        public void ResetDatabaseAsync()
        {
            Factory.EnsureDeleteAndCreateDatabase();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Client.Dispose();
            Factory.Dispose();
        }     

    }
}