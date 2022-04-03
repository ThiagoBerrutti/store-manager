using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Persistence.Repositories.Extensions;
using StoreAPI.Services.Communication;
using StoreAPI.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly UserParametersValidator _userParametersValidator;

        public UserService(IRoleService roleService, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, LinkGenerator linkGenerator)
        {
            _roleService = roleService;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _linkGenerator = linkGenerator;
            _userUpdateValidator = new UserUpdateValidator();
            _changePasswordValidator = new ChangePasswordValidator();
            _userParametersValidator = new UserParametersValidator();
        }

        public async Task<ServiceResponse<PaginatedList<UserReadDto>>> GetAllDtoPaginatedAsync(UserParametersDto parameters)
        {
            var validationResult = _userParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<PaginatedList<UserReadDto>>(validationResult)
                    .SetDetail($"Invalid query string parameters. See '{ServiceResponse.ErrorKey}' for more details");
            }

            var earliestDob = parameters.EarliestDateToSearch();
            var latestDob = parameters.LatestDateToSearch();

            Expression<Func<User, bool>> expression =
                    u =>
                        //Name
                        (string.IsNullOrEmpty(parameters.Name) ||
                            u.FirstName.ToLower().Contains(parameters.Name.ToLower()) || u.LastName.ToLower().Contains(parameters.Name.ToLower())) &&
                        //MinDateOfBirth
                        ((!parameters.MinDateOfBirth.HasValue && !parameters.MaxAge.HasValue) ||
                            u.DateOfBirth >= earliestDob) &&
                        //MaxDateOfBirth
                        ((!parameters.MaxDateOfBirth.HasValue && !parameters.MinAge.HasValue) ||
                            u.DateOfBirth <= latestDob) &&
                        //UserName
                        (string.IsNullOrEmpty(parameters.UserName) ||
                            u.UserName.ToLower().Contains(parameters.UserName.ToLower())) &&
                        //RoleId
                        (u.Roles.Select(u => u.Id)
                                    .Where(id => parameters.RoleId.Contains(id))
                                    .Count() == parameters.RoleId.Count());

            var page = await _userRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);

            var dto = _mapper.Map<PaginatedList<User>, PaginatedList<UserReadDto>>(page);
            var result = new ServiceResponse<PaginatedList<UserReadDto>>(dto);

            return result;
        }


        public async Task<ServiceResponse<User>> GetByUserNameAsync(string userName)
        {
            var validationResult = new ValidationResult().ValidateUserName(userName);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<User>(validationResult)
                        .SetDetail($"Invalid user data. See '{ServiceResponse.ErrorKey} for more details");
            }

            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                return new FailedServiceResponse<User>()
                    .SetTitle("User not found")
                    .SetDetail($"User '{userName}' not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }

            var result = new ServiceResponse<User>(user);

            return result;
        }


        public async Task<ServiceResponse<User>> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new FailedServiceResponse<User>()
                    .SetTitle("User not found")
                    .SetDetail($"User [Id = {id}] not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }

            var result = new ServiceResponse<User>(user);

            return result;
        }


        public async Task<ServiceResponse<UserDetailedReadDto>> GetDtoByIdAsync(int id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "User Id");
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<UserDetailedReadDto>(validationResult)
                        .SetDetail($"Invalid user data. See '{ServiceResponse.ErrorKey} for more details");
            }

            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new FailedServiceResponse<UserDetailedReadDto>(response.Error);
            }

            var user = response.Data;

            var dto = _mapper.Map<UserDetailedReadDto>(user);
            var result = new ServiceResponse<UserDetailedReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<PaginatedList<RoleReadDto>>> GetAllRolesFromUserPaginatedAsync(int id, QueryStringParameterDto parameters)
        {
            var validationResult = new ValidationResult().ValidateId(id, "User Id");
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<PaginatedList<RoleReadDto>>(validationResult)
                        .SetDetail($"Invalid user data. See '{ServiceResponse.ErrorKey} for more details");
            }

            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new FailedServiceResponse<PaginatedList<RoleReadDto>>(response.Error);
            }

            var user = response.Data;
            var rolesFromUser = user.Roles;

            var paginatedUsers = rolesFromUser
                    .OrderBy(r => r.Id)
                    .ToPaginatedList(parameters.PageNumber, parameters.PageSize);

            var dto = _mapper.Map<PaginatedList<RoleReadDto>>(paginatedUsers);
            var result = new ServiceResponse<PaginatedList<RoleReadDto>>(dto);

            return result;
        }


        public async Task<ServiceResponse<IList<string>>> GetRolesNamesAsync(string userName)
        {
            var validationResult = new ValidationResult().ValidateUserName(userName);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<IList<string>>(validationResult)
                        .SetDetail($"Invalid user data. See '{ServiceResponse.ErrorKey} for more details");
            }

            var response = await GetByUserNameAsync(userName);
            if (!response.Success)
            {
                return new FailedServiceResponse<IList<string>>(response.Error);
            }

            var user = response.Data;
            var roles = await _userRepository.GetRolesNamesAsync(user);

            var result = new ServiceResponse<IList<string>>(roles);

            return result;
        }


        //called by AuthService use only
        public async Task<ServiceResponse<IdentityResult>> CreateAsync(User user, string password)
        {
            var createResponse = await _userRepository.CreateAsync(user, password);
            if (!createResponse.Succeeded)
            {
                return new FailedServiceResponse<IdentityResult>(createResponse);
            }

            var result = new ServiceResponse<IdentityResult>(createResponse);

            return result;
        }


        public async Task<ServiceResponse<UserReadDto>> AddToRoleAsync(int id, int roleId)
        {
            var validationResult = new ValidationResult()
                        .ValidateId(id, "User Id")
                        .ValidateId(roleId, "Role Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<UserReadDto>(validationResult)
                        .SetDetail($"Invalid data. See '{ServiceResponse.ErrorKey} for more details");
            }

            var adminRoleId = AppConstants.Roles.Admin.Id;
            var adminUserId = AppConstants.Users.Admin.Id;
            var adminRoleName = AppConstants.Roles.Admin.Name;

            var roleResponse = await _roleService.GetByIdAsync(roleId);
            if (!roleResponse.Success)
            {
                return new FailedServiceResponse<UserReadDto>(roleResponse.Error);
            }

            var role = roleResponse.Data;

            if (roleId == adminRoleId)
            {
                var currentUser = await GetCurrentUserAsync();
                if (currentUser.Id != adminUserId)
                {
                    return new FailedServiceResponse<UserReadDto>()
                        .SetTitle("Error adding role to user")
                        .SetDetail($"Only root {adminRoleName} [Id = {adminUserId}] can assign {adminRoleName} role")
                        .SetInstance(UserInstance(id));
                }
            }

            var userResponse = await GetByIdAsync(id);
            if (!userResponse.Success)
            {
                return new FailedServiceResponse<UserReadDto>(userResponse.Error);
            }

            var user = userResponse.Data;
            var hasRole = user.Roles.Contains(role);

            if (hasRole)
            {
                return new FailedServiceResponse<UserReadDto>()
                    .SetTitle("Error adding user to role")
                    .SetDetail($"User already assigned to role '{role.Name}'.")
                    .SetInstance(UserInstance(id));
            }

            var addRoleResult = await _userRepository.AddToRoleAsync(user, role.Name);
            if (!addRoleResult.Succeeded)
            {
                return new FailedServiceResponse<UserReadDto>(addRoleResult)
                    .SetTitle("Error adding user to role")
                    .SetDetail($"User not assigned to role '{role.Name}'. See '{ServiceResponse.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }

            var dto = _mapper.Map<UserReadDto>(user);
            var result = new ServiceResponse<UserReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<UserReadDto>> RemoveFromRoleAsync(int id, int roleId)
        {
            var validationResult = new ValidationResult()
                    .ValidateId(id, "User Id")
                    .ValidateId(roleId, "Role Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<UserReadDto>(validationResult)
                        .SetDetail($"Invalid data. See '{ServiceResponse.ErrorKey} for more details");
            }

            var adminUserId = AppConstants.Users.Admin.Id;
            var adminRoleName = AppConstants.Roles.Admin.Name;
            var adminRoleId = AppConstants.Roles.Admin.Id;
            var currentUser = await GetCurrentUserAsync();

            if (id == adminUserId && roleId == adminRoleId)
            {
                return new FailedServiceResponse<UserReadDto>()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Cannot remove '{adminRoleName}' role from root admin.")
                    .SetInstance(UserInstance(id));
            }

            if (roleId == adminRoleId && currentUser.Id != adminUserId)
            {
                return new FailedServiceResponse<UserReadDto>()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Only root {adminRoleName} [Id = {adminUserId}] can remove {adminRoleName} role")
                    .SetInstance(UserInstance(id));
            }

            var roleResponse = await _roleService.GetByIdAsync(roleId);
            if (!roleResponse.Success)
            {
                return new FailedServiceResponse<UserReadDto>(roleResponse.Error);
            }

            var roleToRemove = roleResponse.Data;

            var userResponse = await GetByIdAsync(id);
            if (!userResponse.Success)
            {
                return new FailedServiceResponse<UserReadDto>(roleResponse.Error);
            }

            var userToRemoveRole = userResponse.Data;
            var hasRole = userToRemoveRole.Roles.Contains(roleToRemove);

            if (!hasRole)
            {
                return new FailedServiceResponse<UserReadDto>()
                    .SetTitle("Error removing role from user")
                    .SetDetail($"User not assigned to role '{roleToRemove.Name}'")
                    .SetInstance(UserInstance(id));
            }

            var removeRoleResult = await _userRepository.RemoveFromRoleAsync(userToRemoveRole, roleToRemove.Name);
            if (!removeRoleResult.Succeeded)
            {
                return new FailedServiceResponse<UserReadDto>(removeRoleResult)
                    .SetTitle("Error removing role from user")
                    .SetDetail($"Error removing user from role '{roleToRemove.Name}'. See '{ServiceResponse.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }

            var userToReturnResponse = await GetByIdAsync(id);
            if (!userToReturnResponse.Success)
            {
                return new FailedServiceResponse<UserReadDto>(userToReturnResponse.Error);
            }

            var userToReturn = userToReturnResponse.Data;
            var dto = _mapper.Map<UserReadDto>(userToReturn);

            var result = new ServiceResponse<UserReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<UserDetailedReadDto>> GetCurrentUserDtoAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                return new FailedServiceResponse<UserDetailedReadDto>()
                    .SetTitle("Current user not found");
            }
            var dto = _mapper.Map<UserDetailedReadDto>(user);
            var result = new ServiceResponse<UserDetailedReadDto>(dto);

            return result;
        }


        protected async Task<User> GetCurrentUserAsync()
        {
            var user = await _userRepository.GetCurrentUserAsync();

            return user;
        }


        public async Task<ServiceResponse<UserReadDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var validationResult = _userUpdateValidator
                    .Validate(userUpdateDto)
                    .ValidateId(id, "User Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse<UserReadDto>(validationResult);
            }
            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new FailedServiceResponse<UserReadDto>(response.Error);
            }

            var user = response.Data;
            _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);

            var updateResult = await _userRepository.UpdateUserAsync(user);
            if (!updateResult.Succeeded)
            {
                return new FailedServiceResponse<UserReadDto>(updateResult)
                    .SetTitle("Error updating user")
                    .SetDetail($"See '{ServiceResponse.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(user.Id));
            }

            var updatedUserResponse = await GetByIdAsync(id);
            if (!updatedUserResponse.Success)
            {
                return new FailedServiceResponse<UserReadDto>(updatedUserResponse.Error);
            }

            var updatedUser = updatedUserResponse.Data;
            var dto = _mapper.Map<UserReadDto>(updatedUser);

            var result = new ServiceResponse<UserReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse> ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto)
        {
            var validationResult = _changePasswordValidator.Validate(changePasswordDto)
                    .ValidateId(id, "User Id");

            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse(validationResult)
                    .SetDetail($"Invalid passwords. See '{ServiceResponse.ErrorKey}' for more details");
            }

            var userResponse = await GetByIdAsync(id);
            if (!userResponse.Success)
            {
                return new FailedServiceResponse(userResponse.Error);
            }

            var user = userResponse.Data;

            var changePasswordResponse = await _userRepository.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!changePasswordResponse.Succeeded)
            {
                return new FailedServiceResponse(changePasswordResponse)
                    .SetTitle("Error changing password")
                    .SetDetail($"See '{ServiceResponse.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }

            return new ServiceResponse();
        }


        public async Task<ServiceResponse> ChangeCurrentUserPasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var validationResult = _changePasswordValidator.Validate(changePasswordDto);
            if (!validationResult.IsValid)
            {
                return new FailedServiceResponse(validationResult)
                    .SetDetail($"Invalid passwords. See '{ServiceResponse.ErrorKey}' for more details")
                    .SetInstance(UserInstance(currentUser.Id));
            }

            var changePasswordResponse = await _userRepository.ChangePasswordAsync(currentUser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!changePasswordResponse.Succeeded)
            {
                return new FailedServiceResponse(changePasswordResponse)
                    .SetTitle("Error changing password")
                    .SetDetail($"See '{ServiceResponse.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(currentUser.Id));
            }

            var result = new ServiceResponse();

            return result;
        }


        public async Task<ServiceResponse> ResetPasswordAsync(int id, string newPassword)
        {
            var idValidationResult = new ValidationResult()
                    .ValidateId(id, "User Id");

            var passwordValidationResult = new ValidationResult()
                    .ValidatePassword(newPassword, "NewPassword", true);

            if (!string.IsNullOrEmpty(newPassword) && !passwordValidationResult.IsValid)
            {
                if (!idValidationResult.IsValid)
                {
                    idValidationResult.AddFailuresFrom(passwordValidationResult);
                    return new FailedServiceResponse(idValidationResult);
                }
            }

            var userResponse = await GetByIdAsync(id);
            if (!userResponse.Success)
            {
                return new FailedServiceResponse(userResponse);
            }

            var user = userResponse.Data;

            if (string.IsNullOrEmpty(newPassword))
            {
                newPassword = user.UserName; // for simplicity
            }

            var resetPasswordResponse = await _userRepository.ResetPasswordAsync(user, newPassword);
            if (!resetPasswordResponse.Succeeded)
            {
                return new FailedServiceResponse(resetPasswordResponse)
                    .SetTitle("Error reseting password")
                    .SetDetail($"See '{ServiceResponse.ErrorKey}' property for more details")
                    .SetInstance(UserInstance(id));
            }

            return new ServiceResponse();
        }

        private string UserInstance(object id)
        {
            return _linkGenerator.GetPathByName(nameof(Controllers.UserController.GetUserById), new { id });
        }


        public async Task<ServiceResponse> ResetTestUsers()
        {
            await _userRepository.ResetTestUsers();
            await _unitOfWork.CompleteAsync();

            return new ServiceResponse();
        }
    }
}