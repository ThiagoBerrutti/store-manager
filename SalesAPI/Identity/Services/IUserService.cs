using Microsoft.AspNetCore.Identity;
using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public interface IUserService
    {
        public Task<AuthResponse<SignInResult>> Login(UserLoginDto userLogin);

        public Task<AuthResponse<IdentityResult>> Register(UserRegisterDto userDto);

        public Task<UserRegisterDto> GetUserByUserName(string userName);

        public Task<UserViewModel> AddToRole(string userName, string roleName);

        public Task<IEnumerable<UserViewModel>> GetAllAsync();

        public Task<UserViewModel> GetCurrentUser();
    }
}