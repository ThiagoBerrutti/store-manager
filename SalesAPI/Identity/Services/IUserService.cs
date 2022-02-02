using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public interface IUserService
    {
        public Task<AuthResponse> AuthenticateAsync(UserLoginDto userLogin);

        public Task<AuthResponse> AuthenticateAsync(UserRegisterDto userRegisterDto);

        public Task<AuthResponse> RegisterAsync(UserRegisterDto userDto);

        public Task<UserViewModel> GetDtoByUserNameAsync(string userName);

        public Task<User> GetByUserNameAsync(string userName);

        public Task<UserViewModel> GetDtoCurrentUserAsync();

        public Task<UserViewModel> GetDtoByIdAsync(int id);

        public Task<User> GetByIdAsync(int id);

        public Task<IEnumerable<UserViewModel>> GetAllDtoAsync();

        public Task ChangePasswordAsync(string userName, string currentPassword, string newPassword);

        public Task ChangeCurrentUserPasswordAsync(string currentPassword, string newPassword);

        public Task ResetPasswordAsync(string userName, string newPassword);

        public Task<UserViewModel> AddToRoleAsync(int id, int userId);

        public Task<UserViewModel> RemoveFromRoleAsync(int id, int userId);

    }
}