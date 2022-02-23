using StoreAPI.Dtos;
using StoreAPI.Identity;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IAuthService
    {
        public Task<string> GenerateJWTAsync(User user);

        public Task<AuthResponse> RegisterAsync(UserRegisterDto userDto);

        public Task<AuthResponse> AuthenticateAsync(UserLoginDto userLogin);

        public Task<AuthResponse> AuthenticateAsync(UserRegisterDto userRegisterDto);
    }
}