using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.TestUser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class TestAccountService : ITestAccountService
    {
        private readonly IMapper _mapper;

        public TestAccountService(IMapper mapper)
        {
            _mapper = mapper;
        }



        public async Task<UserRegisterDto> GetRandomUser()
        {
            var randomUser = await FetchUser();
            if (randomUser is null)
            {
                return TestAccountUserRegisterFactory.Produce(); // fallback data
            }

            var userRegisterDto = _mapper.Map<UserRegisterDto>(randomUser);
            userRegisterDto.UserName = StringFormatter.RemoveAccents(userRegisterDto.FirstName) + RandomUserNameNumber();
            userRegisterDto.Password = "test";

            return userRegisterDto;
        }


        private async Task<RandomedUser> FetchUser()
        {
            var randomuserApiUrl = "https://randomuser.me/api/?nat=br&inc=name,dob";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponse = await client.GetAsync(randomuserApiUrl);

            var formatters = new List<MediaTypeFormatter>()
            {
                new JsonMediaTypeFormatter()
            };

            var response = await httpResponse.Content.ReadAsAsync<Response>(formatters);
            var user = response.Results[0];

            return user;
        }


        private static string RandomUserNameNumber()
        {
            var random = new Random();
            var number = random.Next(int.MaxValue).ToString();

            return number;
        }
    }
}