using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Dtos;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IRoleService _roleService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(SignInManager<User> signInManager, IRoleService roleService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllDtoAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var usersModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return usersModel;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _userRepository.GetByNameAsync(userName);
            if (user == null)
            {
                throw new DomainNotFoundException($"User ['{userName}'] not found.");
            }

            return user;
        }

        public async Task<UserViewModel> GetDtoByUserNameAsync(string userName)
        {
            var user = await GetByUserNameAsync(userName);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new DomainNotFoundException($"User [Id = {id}] not found.");
            }

            return user;
        }

        public async Task<UserViewModel> GetDtoByIdAsync(int id)
        {
            var user = await GetByIdAsync(id);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        public async Task<AuthResponse> RegisterAsync(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var result = await _userRepository.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException("Error on user registration.", result.Errors);
            }

            var appUser = await _userRepository.GetByNameAsync(user.UserName);
            var userLogin = _mapper.Map<UserLoginDto>(appUser);
            userLogin.Password = userDto.Password;

            var loginResult = await AuthenticateAsync(userLogin);
            var userModel = _mapper.Map<UserViewModel>(appUser);

            return new AuthResponse(userModel, loginResult.Token);
        }


        public async Task<AuthResponse> AuthenticateAsync(UserLoginDto userLogin)
        {
            var user = await _userRepository.GetByNameAsync(userLogin.UserName);
            if (user == null)
            {
                throw new IdentityException("Invalid username or password.");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
            if (!signInResult.Succeeded)
            {
                throw new IdentityException("Invalid username or password.");
            }

            var appUser = await _userRepository.GetByNameAsync(userLogin.UserName);

            var token = await _tokenService.GenerateJWTAsync(appUser);
            var userToReturn = _mapper.Map<UserViewModel>(appUser);

            return new AuthResponse(userToReturn, token);
        }


        public async Task<AuthResponse> AuthenticateAsync(UserRegisterDto userRegisterDto)
        {
            var userLoginDto = _mapper.Map<UserLoginDto>(userRegisterDto);
            return await AuthenticateAsync(userLoginDto);
        }


        public async Task<UserViewModel> AddToRoleAsync(int id, int roleId)
        {
            var role = await _roleService.GetByIdAsync(roleId);
            if (role.Name == "Administrator" && !_httpContextAccessor.HttpContext.User.IsInRole("Administrator"))
            {
                throw new IdentityException($"Error removing user from role ['{role.Name}']: Administrator role required.");
            }

            var user = await GetByIdAsync(id);

            var hasRole = user.Roles.Contains(role);
            if (hasRole)
            {
                throw new IdentityException($"User already assigned to role [\"{role.Name}\"].");
            }
            
            var result = await _userRepository.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                throw new IdentityException($"Error adding user ['{user.UserName}'] to role ['{role.Name}'] .", result.Errors);
            }

            var userModel = _mapper.Map<UserViewModel>(user);
            return userModel;
        }


        public async Task<UserViewModel> RemoveFromRoleAsync(int id, int roleId)
        {
            var role = await _roleService.GetByIdAsync(roleId);
            if (role.Name == "Administrator" && !_httpContextAccessor.HttpContext.User.IsInRole("Administrator"))
            {
                throw new IdentityException($"Error removing user from role ['{role.Name}']: Administrator role required.");
            }

            var user = await GetByIdAsync(id);
            if (user.Id == 1)
            {
                throw new IdentityException($"Error removing user from role ['{role.Name}'].", new List<IdentityError> { new IdentityError { Description = "Cannot remove Administrator role from root user." } });

            }

            var hasRole = user.Roles.Contains(role);
            if (!hasRole)
            {
                throw new IdentityException($"User not assigned to role ['{role.Name}'].");
            }

            var result = await _userRepository.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                throw new IdentityException($"Error removing user ['{user.UserName}'] to role ['{role.Name}'] .", result.Errors);
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
            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                throw new IdentityException("User not logged in");
            }

            var user = await _userRepository.GetCurrentUserAsync();

            return user;
        }


        public async Task ChangePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            var user = await GetByUserNameAsync(userName);
            var result = await _userRepository.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException("Error changing password.", result.Errors);
            }
        }


        public async Task ChangeCurrentUserPasswordAsync(string currentPassword, string newPassword)
        {
            var currentUser = await GetCurrentUserAsync();
            var result = await _userRepository.ChangePasswordAsync(currentUser, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException("Error changing password.", result.Errors);
            }
        }


        public async Task ResetPasswordAsync(string userName, string newPassword)
        {
            var user = await GetByUserNameAsync(userName);

            var result = await _userRepository.ResetPasswordAsync(user, newPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException("Error reseting password.", result.Errors);
            }
        }
    }
}