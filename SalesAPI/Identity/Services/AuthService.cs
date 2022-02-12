using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using SalesAPI.Dtos;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Validations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace SalesAPI.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        //private readonly IUserRegisterValidator _userRegisterValidator;
        private readonly AppSettings _appSettings;

        public AuthService(SignInManager<User> signInManager, IOptions<AppSettings> appSettings, IUserService userService, IMapper mapper, LinkGenerator linkGenerator)
            //, IUserRegisterValidator userRegisterValidator )
        {
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            //_userRegisterValidator = userRegisterValidator;
        }



        public async Task<AuthResponse> RegisterAsync(UserRegisterDto userDto)
        {
            //var validationRes = _userRegisterValidator.Validate(userDto);
            var x = new UserRegisterValidator();
            
            

            var user = _mapper.Map<User>(userDto);

            var result = await _userService.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error on user registration")
                    .SetDetail("Registration cancelled. See 'errors' property for more details")
                    .SetErrors(result.Errors);
            }

            var appUser = await _userService.GetByUserNameAsync(user.UserName);
            var userLogin = _mapper.Map<UserLoginDto>(appUser);
            userLogin.Password = userDto.Password;

            var loginResult = await AuthenticateAsync(userLogin);
            var userModel = _mapper.Map<UserAuthViewModel>(appUser);

            return new AuthResponse(userModel, loginResult.Token);
        }


        public async Task<AuthResponse> AuthenticateAsync(UserLoginDto userLogin)
        {
            var user = await _userService.GetByUserNameAsync(userLogin.UserName);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    //var instance = _linkGenerator.GetPathByName(nameof(Controllers.UserController.GetUserById), new { id = user.Id });
                    throw new IdentityException()
                        .SetTitle("Error authenticating user")
                        .SetDetail($"User locked out until {user.LockoutEnd:G}");
                        //.SetInstance(instance);
                }

                if (signInResult.IsNotAllowed)
                {
                    //var instance = _linkGenerator.GetPathByName(nameof(Controllers.UserController.GetUserById), new { id = user.Id });
                    throw new IdentityException()
                        .SetTitle("Error authenticating user")
                        .SetDetail($"User not allowed.");
                        //.SetInstance(instance);
                }

                throw new IdentityException().SetTitle("Error authenticating user");
            }

            var appUser = await _userService.GetByUserNameAsync(userLogin.UserName);

            var token = await GenerateJWTAsync(appUser);
            var userToReturn = _mapper.Map<UserAuthViewModel>(appUser);

            return new AuthResponse(userToReturn, token);
        }


        public async Task<AuthResponse> AuthenticateAsync(UserRegisterDto userRegisterDto)
        {
            var userLoginDto = _mapper.Map<UserLoginDto>(userRegisterDto);
            return await AuthenticateAsync(userLoginDto);
        }


        public async Task<string> GenerateJWTAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userService.GetRolesNamesAsync(user.UserName);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var encodedPrivateKey = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var key = new SymmetricSecurityKey(encodedPrivateKey);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expires),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}