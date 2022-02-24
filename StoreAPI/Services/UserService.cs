using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleService _roleService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LinkGenerator _linkGenerator;

        private readonly UserUpdateValidator _userUpdateValidator;
        private readonly ChangePasswordValidator _changePasswordValidator;

        public UserService(IRoleService roleService, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, LinkGenerator linkGenerator)
        {
            _roleService = roleService;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _linkGenerator = linkGenerator;
            _userUpdateValidator = new UserUpdateValidator();
            _changePasswordValidator = new ChangePasswordValidator();
        }



        public async Task<IEnumerable<UserReadDto>> GetAllDtoAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var usersModel = _mapper.Map<IEnumerable<UserReadDto>>(users);

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


        public async Task<UserReadDto> GetDtoByUserNameAsync(string userName)
        {
            var user = await GetByUserNameAsync(userName);
            var userViewModel = _mapper.Map<UserReadDto>(user);

            return userViewModel;
        }

        public async Task<IEnumerable<UserReadDto>> SearchAsync(string search)
        {
            var result = await _userRepository.SearchAsync(search);

            var usersDto = _mapper.Map<IEnumerable<UserReadDto>>(result);

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


        public async Task<UserReadDto> GetDtoByIdAsync(int id)
        {
            var user = await GetByIdAsync(id);
            var userViewModel = _mapper.Map<UserReadDto>(user);

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


        public async Task<UserReadDto> AddToRoleAsync(int id, int roleId)
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
                throw new IdentityException(result)
                    .SetTitle("Error adding role to user")
                    .SetDetail($"User not assigned to role '{role.Name}'. See '{ExceptionWithProblemDetails.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }

            var userModel = _mapper.Map<UserReadDto>(user);
            return userModel;
        }


        public async Task<UserReadDto> RemoveFromRoleAsync(int id, int roleId)
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
                throw new IdentityException(result)
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Error removing user from role '{roleToRemove.Name}'. See '{ExceptionWithProblemDetails.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }

            var userToReturn = await GetByIdAsync(id);
            var userModel = _mapper.Map<UserReadDto>(userToReturn);

            return userModel;
        }


        public async Task<UserReadDto> GetDtoCurrentUserAsync()
        {
            var user = await GetCurrentUserAsync();
            var userViewModel = _mapper.Map<UserReadDto>(user);

            return userViewModel;
        }


        public async Task<User> GetCurrentUserAsync()
        {
            var user = await _userRepository.GetCurrentUserAsync();

            return user;
        }


        public async Task<UserReadDto> UpdateUserAsync(string userName, UserUpdateDto userUpdateDto)
        {
            var validationResult = _userUpdateValidator.Validate(userUpdateDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid user data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }
            var user = await GetByUserNameAsync(userName);
            _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);

            var result = await _userRepository.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                throw new IdentityException(result)
                    .SetTitle("Error updating user")
                    .SetDetail($"See '{ExceptionWithProblemDetails.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(user.Id));
            }

            var updatedUser = await GetByUserNameAsync(userName);
            var userToResult = _mapper.Map<UserReadDto>(updatedUser);

            return userToResult;
        }


        public async Task ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto)
        {
            var validationResult = _changePasswordValidator.Validate(changePasswordDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid passwords. See '{ExceptionWithProblemDetails.ErrorKey}' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var user = await GetByIdAsync(id);

            var result = await _userRepository.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException(result)
                    .SetTitle("Error changing password")
                    .SetDetail($"See '{ExceptionWithProblemDetails.ErrorKey}' property for more details")
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
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid passwords. See '{ExceptionWithProblemDetails.ErrorKey}' for more details")
                    .SetInstance(UserInstance(currentUser.Id));
            }

            var result = await _userRepository.ChangePasswordAsync(currentUser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException(result)
                    .SetTitle("Error changing password")
                    .SetDetail($"See '{ExceptionWithProblemDetails.ErrorKey}' property for more details")
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
                throw new IdentityException(result)
                    .SetTitle("Error reseting password")
                    .SetDetail($"See '{ExceptionWithProblemDetails.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }
        }

        private string UserInstance(object id)
        {
            return _linkGenerator.GetPathByName(nameof(Controllers.UserController.GetUserById), new { id });
        }


        public async Task ResetTestUsers()
        {
            await _userRepository.ResetTestUsers();
            await _unitOfWork.CompleteAsync();
        }
    }
}