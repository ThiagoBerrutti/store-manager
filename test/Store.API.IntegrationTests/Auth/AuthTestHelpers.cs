//using StoreAPI.Dtos;
//using StoreAPI.Infra;
//using StoreAPI.Persistence;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace Store.API.IntegrationTests.Auth
//{
//    public class AuthTestHelpers
//    {
//        public HttpClient Client { get; set; }
//        public StoreDbContext Context { get; set; }

//        public AuthTestHelpers(HttpClient client, StoreDbContext context)
//        {
//            Client = client;
//            Context = context;
//        }

//        public async Task AuthenticateAsync(UserLoginDto user)
//        {
//            var token = await GetJwtAsync(user);

//            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
//        }


//        public async Task AuthenticateAsAdminAsync()
//        {
//            await AuthenticateAsync(AuthObjects.UserLogins.Admin);
//        }


//        public void LogoutUser()
//        {
//            Client.DefaultRequestHeaders.Authorization = null;
//        }


//        private async Task<string> GetJwtAsync(UserLoginDto user)
//        {
//            var uri = ApiRoutes.Auth.Authenticate;

//            var responseMessage = await Client.PostAsJsonAsync(uri, user);

//            var authResponse = await responseMessage.Content.ReadAsAsync<AuthResponse>();
//            var token = authResponse.Token;

//            return token;
//        }
//    }
//}
