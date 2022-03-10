using Microsoft.AspNetCore.Identity;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using StoreAPI.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public interface IUserService
    {        
        public Task<ServiceResponse<PaginatedList<UserReadDto>>> GetAllDtoPaginatedAsync(UserParametersDto parameters);

        public Task<ServiceResponse<User>> GetByUserNameAsync(string userName);

        public Task<ServiceResponse<UserDetailedReadDto>> GetCurrentUserDtoAsync();

        public Task<ServiceResponse<UserDetailedReadDto>> GetDtoByIdAsync(int id);

        public Task<ServiceResponse<User>> GetByIdAsync(int id);

        public Task<ServiceResponse<PaginatedList<RoleReadDto>>> GetAllRolesFromUserPaginatedAsync(int id, QueryStringParameterDto parameters);
                
        public Task<ServiceResponse<IdentityResult>> CreateAsync(User user, string password);

        public Task<ServiceResponse<UserReadDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);

        public Task<ServiceResponse> ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);

        public Task<ServiceResponse> ChangeCurrentUserPasswordAsync(ChangePasswordDto changePasswordDto);

        public Task<ServiceResponse> ResetPasswordAsync(int id, string newPassword);

        public Task<ServiceResponse<UserReadDto>> AddToRoleAsync(int id, int userId);

        public Task<ServiceResponse<UserReadDto>> RemoveFromRoleAsync(int id, int userId);

        public Task<ServiceResponse<IList<string>>> GetRolesNamesAsync(string userName);

        public Task<ServiceResponse> ResetTestUsers();


    }
}