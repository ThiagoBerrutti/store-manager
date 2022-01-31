using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Models;
using SalesAPI.Services;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var registerResult = await _userService.Register(userDto);
            if (!registerResult.Succeeded)
            {
                return BadRequest(registerResult.Result.Errors);
            }

            //var loginResult = await _userService.Login(userDto);

            return Created(nameof(GetUserByUserName), new { User = registerResult.User, Token = registerResult.Token });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            var result = await _userService.Login(userLogin);
            if (!result.Succeeded)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(new { User = result.User, Token = result.Token });
        }

        [HttpGet(Name = "GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var result = await _userService.GetUserByUserName(userName);

            return Ok(result);
        }
    }
}