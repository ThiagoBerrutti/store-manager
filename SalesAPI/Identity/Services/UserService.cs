using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(SignInManager<User> signInManager, RoleManager<Role> roleManager, UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthResponse<IdentityResult>> Register(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var result = await _userManager.CreateAsync(user,userDto.Password);

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

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);//.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if (!signInResult.Succeeded)
            {
                return new AuthResponse<SignInResult>(signInResult);
            }

            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());

            var token = await _tokenService.GenerateJWTAsync(appUser);
            var userToReturn = _mapper.Map<UserViewModel>(appUser);

            return new AuthResponse<SignInResult>(userToReturn, token,signInResult);
        }

        public async Task<UserRegisterDto> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userDto = _mapper.Map<UserRegisterDto>(user);

            return userDto;
        }

       

    }
}