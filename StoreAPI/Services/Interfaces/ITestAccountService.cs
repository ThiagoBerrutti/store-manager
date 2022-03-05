using StoreAPI.Dtos;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface ITestAccountService
    {
        public Task<UserRegisterDto> GetRandomUser();
    }
}