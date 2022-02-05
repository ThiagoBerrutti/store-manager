using SalesAPI.Dtos;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public interface IAuthService
    {
        public Task<string> GenerateJWTAsync(User user);

        public Task<AuthResponse> RegisterAsync(UserRegisterDto userDto);

        public Task<AuthResponse> AuthenticateAsync(UserLoginDto userLogin);

        public Task<AuthResponse> AuthenticateAsync(UserRegisterDto userRegisterDto);
    }
}