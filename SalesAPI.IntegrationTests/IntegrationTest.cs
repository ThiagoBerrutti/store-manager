using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using SalesAPI.Dtos;
using SalesAPI.Helpers;
using SalesAPI.Identity;
using SalesAPI.Persistence;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SalesAPI.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<SalesDbContext>));
                        services.AddDbContext<SalesDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
        }




        protected async Task RegisterAsync(string userName)
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetRegisterJwtAsync("userName"));
        }


        protected async Task AuthenticateAsync(string userName)
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync(userName));

        }

        protected async Task<string> GetJwtAsync(string userName)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Auth.Authenticate, new UserLoginDto
            {
                UserName = userName,
                Password = "string"
            });

            var registrationResponse = await response.Content.ReadAsJsonAsync<AuthResponse>();
            var token = registrationResponse.Token;

            return token;
        }


        protected async Task<string> GetRegisterJwtAsync(string userName)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Auth.Register, new UserRegisterDto
            {
                UserName = userName,
                DateOfBirth = new DateTime(1984, 12, 16),
                FirstName = "test",
                LastName = "admin",
                Password = "string"
            });

            var registrationResponse = await response.Content.ReadAsJsonAsync<AuthResponse>();
            var token = registrationResponse.Token;

            return token;
        }
    }


    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
    }
}