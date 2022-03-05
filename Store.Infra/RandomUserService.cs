using StoreAPI.TestUser;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class RandomUser2Service : IRandomUserService
    {
        //private readonly IMapper _mapper;

        //public RandomUserService(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}



        public async Task<RandomedUser> FetchUser()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponse = await client.GetAsync("https://randomuser.me/api/?nat=br");

            var formatters = new List<MediaTypeFormatter>()
            {
                new JsonMediaTypeFormatter()
            };
            var response = await httpResponse.Content.ReadAsAsync<Response>(formatters);
            var user = response.Results[0];

            return user;
        }
    }
}