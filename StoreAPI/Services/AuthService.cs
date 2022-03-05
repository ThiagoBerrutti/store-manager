using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Validations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace StoreAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        private readonly UserLoginValidator _userLoginValidator;
        private readonly UserRegisterValidator _userRegisterValidator;

        public AuthService(SignInManager<User> signInManager, IOptions<AppSettings> appSettings, IUserService userService, IMapper mapper)
        {
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            _mapper = mapper;
            _userLoginValidator = new UserLoginValidator();
            _userRegisterValidator = new UserRegisterValidator();
        }



        public async Task<AuthResponse> RegisterAsync(UserRegisterDto userDto)
        {
            var validationResult = _userRegisterValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid user data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var user = _mapper.Map<User>(userDto);

            var result = await _userService.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException(result)
                    .SetTitle("Error on user registration")
                    .SetDetail($"See '{ExceptionWithProblemDetails.ErrorKey}' property for more details");
            }

            var appUser = await _userService.GetByUserNameAsync(user.UserName);
            var userLogin = _mapper.Map<UserLoginDto>(appUser);
            userLogin.Password = userDto.Password;

            var loginResult = await AuthenticateAsync(userLogin);
            var userModel = _mapper.Map<UserAuthDto>(appUser);

            return new AuthResponse(userModel, loginResult.Token);
        }


        public async Task<AuthResponse> AuthenticateAsync(UserLoginDto userDto)
        {
            var validationResult = _userLoginValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid user data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }
            var user = await _userService.GetByUserNameAsync(userDto.UserName);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    throw new IdentityException()
                        .SetTitle("Error authenticating user")
                        .SetDetail($"User locked out until {user.LockoutEnd:G}");
                }

                if (signInResult.IsNotAllowed)
                {
                    throw new IdentityException()
                        .SetTitle("Error authenticating user")
                        .SetDetail($"User not allowed.");
                }

                throw new IdentityException().SetTitle("Incorrect user/password");
            }

            var appUser = await _userService.GetByUserNameAsync(userDto.UserName);

            var token = await GenerateJWTAsync(appUser);
            var userToReturn = _mapper.Map<UserAuthDto>(appUser);

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