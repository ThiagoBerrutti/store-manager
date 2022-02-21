using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using SalesAPI.Infra;
using SalesAPI.Services;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(UserRegisterDto userDto)
        {
            var registerResponse = await _authService.RegisterAsync(userDto);
            var authenticateResponse = await _authService.AuthenticateAsync(userDto);

            return CreatedAtRoute(nameof(UserController.GetUserById), new { authenticateResponse.User.Id }, authenticateResponse);
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponse>> Authenticate(UserLoginDto userLogin)
        {
            var authResponse = await _authService.AuthenticateAsync(userLogin);

            return Ok(authResponse);
        }


        [AllowAnonymous]
        [HttpPost("authenticate/test/admin")]
        public async Task<ActionResult<AuthResponse>> AuthenticateTestAdminUser()
        {
            await _userService.ResetTestUsers();

            var testAdminLogin = new UserLoginDto
            {
                Password = AppConstants.Users.Admin.Password,
                UserName = AppConstants.Users.Admin.UserName
            };

            var authResponse = await _authService.AuthenticateAsync(testAdminLogin);

            return Ok(authResponse);
        }


        [AllowAnonymous]
        [HttpPost("authenticate/test/manager")]
        public async Task<ActionResult<AuthResponse>> AuthenticateTestManagerUser()
        {
            await _userService.ResetTestUsers();

            var testManagerLogin = new UserLoginDto
            {
                Password = AppConstants.Users.Manager.Password,
                UserName = AppConstants.Users.Manager.UserName
            };

            var authResponse = await _authService.AuthenticateAsync(testManagerLogin);

            return Ok(authResponse);
        }


        [AllowAnonymous]
        [HttpPost("authenticate/test/stock")]
        public async Task<ActionResult<AuthResponse>> AuthenticateTestStockUser()
        {
            await _userService.ResetTestUsers();

            var testStockLogin = new UserLoginDto
            {
                Password = AppConstants.Users.Stock.Password,
                UserName = AppConstants.Users.Stock.UserName
            };

            var authResponse = await _authService.AuthenticateAsync(testStockLogin);

            return Ok(authResponse);
        }


        [AllowAnonymous]
        [HttpPost("authenticate/test/seller")]
        public async Task<ActionResult<AuthResponse>> AuthenticateTestSellerUser()
        {
            await _userService.ResetTestUsers();

            var testSellerLogin = new UserLoginDto
            {
                Password = AppConstants.Users.Seller.Password,
                UserName = AppConstants.Users.Seller.UserName
            };

            var authResponse = await _authService.AuthenticateAsync(testSellerLogin);

            return Ok(authResponse);
        }


        [HttpPost("authenticate/test/public")]
        public async Task<ActionResult<AuthResponse>> AuthenticateTestPublicUser()
        {
            await _userService.ResetTestUsers();

            var testPublicLogin = new UserLoginDto
            {
                Password = AppConstants.Users.Public.Password,
                UserName = AppConstants.Users.Public.UserName
            };

            var authResponse = await _authService.AuthenticateAsync(testPublicLogin);

            return Ok(authResponse);
        }


    }
}