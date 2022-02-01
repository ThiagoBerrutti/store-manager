using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Dtos;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(SignInManager<User> signInManager, RoleManager<Role> roleManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthResponse<IdentityResult>> Register(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponse<IdentityResult>(result);
            }

            var appUser = await _userManager.FindByNameAsync(user.UserName);
            var userLogin = _mapper.Map<UserLoginDto>(appUser);
            userLogin.Password = userDto.Password;

            var loginResult = await Login(userLogin);
            var userModel = _mapper.Map<UserViewModel>(user);

            return new AuthResponse<IdentityResult>(userModel, loginResult.Token, result);
        }

        public async Task<AuthResponse<SignInResult>> Login(UserLoginDto userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            if (user == null)
            {
                throw new IdentityException("Invalid username or password.");
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
            if (!signInResult.Succeeded)
            {
                throw new IdentityException("Invalid username or password.");
            }

            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());

            var token = await _tokenService.GenerateJWTAsync(appUser);
            var userToReturn = _mapper.Map<UserViewModel>(appUser);

            return new AuthResponse<SignInResult>(userToReturn, token, signInResult);
        }

        public async Task<UserRegisterDto> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userDto = _mapper.Map<UserRegisterDto>(user);

            return userDto;
        }

        public async Task<UserViewModel> AddToRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new DomainNotFoundException($"User [Name = '{userName}'] not found.");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new DomainNotFoundException($"Role [Name = '{roleName}'] not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new IdentityException($"Error adding user [Name = '{userName}'] to role [Name = '{roleName}'] .", result.Errors);
            }

            var userModel = _mapper.Map<UserViewModel>(user);
            return userModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _userManager.Users
                .Include(u => u.Roles)
                .ToListAsync();

            var usersModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return usersModel;
        }

        public async Task<UserViewModel> GetCurrentUser()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new IdentityException("User not logged in");
            }

            var user = await _userRepository.GetCurrentUser();

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}