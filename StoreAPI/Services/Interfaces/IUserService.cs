using Microsoft.AspNetCore.Identity;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IUserService
    {        
        public Task<PaginatedList<UserReadDto>> GetAllDtoPaginatedAsync(UserParametersDto parameters);

        public Task<User> GetByUserNameAsync(string userName);

        public Task<UserReadDto> GetCurrentUserDtoAsync();

        public Task<UserDetailedReadDto> GetDtoByIdAsync(int id);

        public Task<User> GetByIdAsync(int id);

        public Task<PaginatedList<RoleReadDto>> GetAllRolesFromUser(int id, QueryStringParameterDto parameters);
                
        public Task<IdentityResult> CreateAsync(User user, string password);

        public Task<UserReadDto> UpdateUserAsync(string userName, UserUpdateDto userUpdateDto);

        public Task ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);

        public Task ChangeCurrentUserPasswordAsync(ChangePasswordDto changePasswordDto);

        public Task ResetPasswordAsync(int id, string newPassword);

        public Task<UserReadDto> AddToRoleAsync(int id, int userId);

        public Task<UserReadDto> RemoveFromRoleAsync(int id, int userId);

        public Task<IList<string>> GetRolesNamesAsync(string userName);

        public Task ResetTestUsers();


    }
}