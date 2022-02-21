using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Identity;
using SalesAPI.Infra;
using SalesAPI.Persistence.Repositories;
using SalesAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleService _roleService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        private readonly UserUpdateValidator _userUpdateValidator;
        private readonly ChangePasswordValidator _changePasswordValidator;

        public UserService(IRoleService roleService, IUserRepository userRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _roleService = roleService;
            _userRepository = userRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _userUpdateValidator = new UserUpdateValidator();
            _changePasswordValidator = new ChangePasswordValidator();
        }



        public async Task<IEnumerable<UserViewModel>> GetAllDtoAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var usersModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return usersModel;
        }


        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                throw new IdentityNotFoundException()
                    .SetTitle("User not found")
                    .SetDetail($"User '{userName}' not found.");
            }

            return user;
        }


        public async Task<UserViewModel> GetDtoByUserNameAsync(string userName)
        {
            var user = await GetByUserNameAsync(userName);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        public async Task<IEnumerable<UserViewModel>> SearchAsync(string search)
        {
            var result = await _userRepository.SearchAsync(search);

            var usersDto = _mapper.Map<IEnumerable<UserViewModel>>(result);

            return usersDto;
        }


        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new IdentityNotFoundException()
                    .SetTitle("User not found")
                    .SetDetail($"User [Id = {id}] not found.");
            }

            return user;
        }


        public async Task<UserViewModel> GetDtoByIdAsync(int id)
        {
            var user = await GetByIdAsync(id);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }


        public async Task<IList<string>> GetRolesNamesAsync(string userName)
        {
            var user = await GetByUserNameAsync(userName);
            var roles = await _userRepository.GetRolesNamesAsync(user);

            return roles;
        }


        //called by AuthService only
        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await _userRepository.CreateAsync(user, password);

            return result;
        }


        public async Task<UserViewModel> AddToRoleAsync(int id, int roleId)
        {
            var adminRoleId = AppConstants.Roles.Admin.Id;
            var adminUserId = AppConstants.Users.Admin.Id;
            var adminRoleName = AppConstants.Roles.Admin.Name;
            var role = await _roleService.GetByIdAsync(roleId);

            if (roleId == adminRoleId)
            {
                var currentUser = await GetCurrentUserAsync();
                if (currentUser.Id != adminUserId)
                {
                    throw new IdentityException()
                        .SetTitle("Error adding role to user")
                        .SetDetail($"Only root {adminRoleName} [Id = {adminUserId}] can assign {adminRoleName} role")
                        .SetInstance(UserInstance(id));
                }
            }

            var user = await GetByIdAsync(id);
            var hasRole = user.Roles.Contains(role);

            if (hasRole)
            {
                throw new IdentityException()
                    .SetTitle("Error adding role to user")
                    .SetDetail($"User already assigned to role '{role.Name}'.")
                    .SetInstance(UserInstance(id));
            }

            var result = await _userRepository.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error adding role to user")
                    .SetDetail($"User not assigned to role '{role.Name}'. See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description))
                    .SetInstance(UserInstance(id));
            }

            var userModel = _mapper.Map<UserViewModel>(user);
            return userModel;
        }


        public async Task<UserViewModel> RemoveFromRoleAsync(int id, int roleId)
        {
            var adminUserId = AppConstants.Users.Admin.Id;
            var adminRoleName = AppConstants.Roles.Admin.Name;
            var adminRoleId = AppConstants.Roles.Admin.Id;
            var currentUser = await GetCurrentUserAsync();

            if (id == adminUserId && roleId == adminRoleId)
            {
                throw new IdentityException()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Cannot remove '{adminRoleName}' role from root admin.")
                    .SetInstance(UserInstance(id));
            }

            if (roleId == adminRoleId && currentUser.Id != adminUserId)
            {
                throw new IdentityException()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Only root {adminRoleName} [Id = {adminUserId}] can remove {adminRoleName} role")
                    .SetInstance(UserInstance(id));
            }

            var roleToRemove = await _roleService.GetByIdAsync(roleId);
            var userToRemoveRole = await GetByIdAsync(id);
            var hasRole = userToRemoveRole.Roles.Contains(roleToRemove);

            if (!hasRole)
            {
                throw new IdentityException()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"User not assigned to role '{roleToRemove.Name}'.")
                    .SetInstance(UserInstance(id));
            }

            var result = await _userRepository.RemoveFromRoleAsync(userToRemoveRole, roleToRemove.Name);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Error removing user from role '{roleToRemove.Name}'. See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description))
                    .SetInstance(UserInstance(id));
            }

            var userToReturn = await GetByIdAsync(id);
            var userModel = _mapper.Map<UserViewModel>(userToReturn);

            return userModel;
        }


        public async Task<UserViewModel> GetDtoCurrentUserAsync()
        {
            var user = await GetCurrentUserAsync();
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }


        public async Task<User> GetCurrentUserAsync()
        {
            var user = await _userRepository.GetCurrentUserAsync();

            return user;
        }


        public async Task<UserViewModel> UpdateUserAsync(string userName, UserUpdateDto userUpdateDto)
        {
            var validationResult = _userUpdateValidator.Validate(userUpdateDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid user data. See 'errors' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var user = await GetByUserNameAsync(userName);
            _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);

            var result = await _userRepository.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error updating user")
                    .SetDetail("See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description))
                    .SetInstance(UserInstance(user.Id));
            }

            var updatedUser = await GetByUserNameAsync(userName);
            var userToResult = _mapper.Map<UserViewModel>(updatedUser);


            return userToResult;
        }


        public async Task ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto)
        {
            var validationResult = _changePasswordValidator.Validate(changePasswordDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid passwords. See 'errors' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var user = await GetByIdAsync(id);

            var result = await _userRepository.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error changing password")
                    .SetDetail("See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description))
                    .SetInstance(UserInstance(id));
            }
        }


        //public async Task ChangeCurrentUserPasswordAsync(string currentPassword, string newPassword)
        public async Task ChangeCurrentUserPasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var validationResult = _changePasswordValidator.Validate(changePasswordDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid passwords. See 'errors' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage))
                    .SetInstance(UserInstance(currentUser.Id));
            }

            var result = await _userRepository.ChangePasswordAsync(currentUser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error changing password")
                    .SetDetail("See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description))
                    .SetInstance(UserInstance(currentUser.Id));
            }
        }


        public async Task ResetPasswordAsync(int id, string newPassword)
        {
            var user = await GetByIdAsync(id);
            if (newPassword == "")
            {
                newPassword = user.UserName; // for simplicity sake
            }

            var result = await _userRepository.ResetPasswordAsync(user, newPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error reseting password")
                    .SetDetail("See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description))
                    .SetInstance(UserInstance(id));
            }
        }

        private string UserInstance(object id)
        {
            return _linkGenerator.GetPathByName(nameof(Controllers.UserController.GetUserById), new { id });
        }

        public async Task ResetTestUsers()
        {
            var adminUser = await GetByUserNameAsync("admin");
            var managerUser = await GetByUserNameAsync("manager");
            var stockUser = await GetByUserNameAsync("stock");
            var sellerUser = await GetByUserNameAsync("seller");
            var publicUser = await GetByUserNameAsync("public");

            await _userRepository.ResetUser(adminUser, AppConstants.Users.Admin.Password, new List<string> { AppConstants.Roles.Admin.Name });
            await _userRepository.ResetUser(managerUser, AppConstants.Users.Manager.Password, new List<string> { AppConstants.Roles.Manager.Name });
            await _userRepository.ResetUser(stockUser, AppConstants.Users.Stock.Password, new List<string> { AppConstants.Roles.Stock.Name });
            await _userRepository.ResetUser(sellerUser, AppConstants.Users.Seller.Password, new List<string> { AppConstants.Roles.Seller.Name });
            await _userRepository.ResetUser(publicUser, AppConstants.Users.Public.Password, new List<string>());

            
        }
    }
}