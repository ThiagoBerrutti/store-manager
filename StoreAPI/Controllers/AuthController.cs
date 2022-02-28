using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// Registration and JWT authentication
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/auth/")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }



        /// <summary>
        /// Registers new user
        /// </summary>
        /// <remarks>Registers a new user and authenticates it</remarks>
        /// <param name="userDto">New user's data</param>
        /// <returns>The user registered and a valid JWT for this user</returns>
        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status201Created, "User successfully registered and authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error registering user", Type = null)]
        public async Task<IActionResult> Register([SwaggerParameter("test description")] UserRegisterDto userDto)
        {
            var registerResponse = await _authService.RegisterAsync(userDto);
            var authenticateResponse = await _authService.AuthenticateAsync(userDto);

            return CreatedAtRoute(nameof(UserController.GetUserById), new { authenticateResponse.User.Id }, authenticateResponse);
        }


        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <remarks>Authenticates an user and generate JWT</remarks>
        /// <param name="userLogin">User's username and password</param>
        /// <returns>A valid JWT for this user</returns>
        [HttpPost("authenticate")]
        [SwaggerResponse(StatusCodes.Status200OK, "User authenticated")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> Authenticate(UserLoginDto userLogin)
        {
            var authResponse = await _authService.AuthenticateAsync(userLogin);

            return Ok(authResponse);
        }


        /// <summary>
        /// Authenticate with a test user with 'Administrator' role assigned
        /// </summary>
        /// <remarks>Resets all test accounts data before authenticate</remarks>
        /// <returns>A valid JWT for test user 'admin'</returns>
        [HttpPost("authenticate/testadmin")]
        [SwaggerResponse(StatusCodes.Status200OK, "Test user 'admin' authenticated")]
        public async Task<IActionResult> AuthenticateTestAdminUser()
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


        /// <summary>
        /// Authenticate with a test user with 'Manager' role assigned
        /// </summary>
        /// <remarks>Resets all test accounts data before authenticate</remarks>
        /// <returns>A valid JWT for test user 'manager'</returns>
        [HttpPost("authenticate/testmanager")]
        [SwaggerResponse(StatusCodes.Status200OK, "Test user 'manager' authenticated")]
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


        /// <summary>
        /// Authenticate with a test user with 'Stock' role assigned
        /// </summary>
        /// <remarks>Resets all test accounts data before authenticate</remarks>
        /// <returns>A valid JWT for test user 'stock'</returns>
        [HttpPost("authenticate/teststock")]
        [SwaggerResponse(StatusCodes.Status200OK, "User 'stock' authenticated")]
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


        /// <summary>
        /// Authenticate with a test user with 'Seller' role assigned
        /// </summary>
        /// <remarks>Resets all test accounts data before authenticate</remarks>
        /// <returns>A valid JWT for test user 'seller'</returns>
        [HttpPost("authenticate/testseller")]
        [SwaggerResponse(StatusCodes.Status200OK, "User 'seller' authenticated")]
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


        /// <summary>
        /// Authenticate with a test user with no roles assigned
        /// </summary>
        /// <remarks>Resets all test accounts data before authenticate</remarks>
        /// <returns>A valid JWT for test user 'public'</returns>
        [HttpPost("authenticate/testpublic")]
        [SwaggerResponse(StatusCodes.Status200OK, "User 'public' authenticated")]
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