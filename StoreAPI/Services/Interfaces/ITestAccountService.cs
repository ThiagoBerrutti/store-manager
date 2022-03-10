using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface ITestAccountService
    {
        public Task<ServiceResponse<UserRegisterDto>> GetRandomUser();

        public Task<ServiceResponse<AuthResponse>> RegisterTestAcc(List<RolesEnum> roleId);
    }
}