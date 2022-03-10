using StoreAPI.Dtos;
using StoreAPI.Services.Communication;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IAuthService
    {
        public Task<ServiceResponse<AuthResponse>> RegisterAsync(UserRegisterDto userDto);

        public Task<ServiceResponse<AuthResponse>> AuthenticateAsync(UserLoginDto userLogin);

        public Task<ServiceResponse<AuthResponse>> AuthenticateAsync(UserRegisterDto userRegisterDto);
    }
}