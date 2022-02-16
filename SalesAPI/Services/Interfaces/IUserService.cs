using Microsoft.AspNetCore.Identity;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IUserService
    {        
        public Task<UserViewModel> GetDtoByUserNameAsync(string userName);

        public Task<IEnumerable<UserViewModel>> SearchAsync(string search);

        public Task<User> GetByUserNameAsync(string userName);

        public Task<UserViewModel> GetDtoCurrentUserAsync();

        public Task<UserViewModel> GetDtoByIdAsync(int id);

        public Task<User> GetByIdAsync(int id);

        public Task<IEnumerable<UserViewModel>> GetAllDtoAsync();

        public Task<IList<string>> GetRolesNamesAsync(string userName);

        public Task<IdentityResult> CreateAsync(User user, string password);

        public Task<UserViewModel> UpdateUserAsync(string userName, UserUpdateDto userUpdateDto);

        public Task ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);

        public Task ChangeCurrentUserPasswordAsync(ChangePasswordDto changePasswordDto);

        public Task ResetPasswordAsync(int id, string newPassword);

        public Task<UserViewModel> AddToRoleAsync(int id, int userId);

        public Task<UserViewModel> RemoveFromRoleAsync(int id, int userId);

    }
}