using StoreAPI.Dtos;
using StoreAPI.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface ITestAccountService
    {
        public Task<UserRegisterDto> GetRandomUser();

        public Task<AuthResponse> RegisterTestAcc(List<RolesEnum> roleId);
    }
}