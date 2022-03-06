using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Helpers;
using StoreAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
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
    [ProducesErrorResponseType(typeof(void))]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ITestAccountService _testUserService;

        public AuthController(IAuthService authService, IUserService userService, IRoleService roleService, ITestAccountService testUserService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
            _testUserService = testUserService;
        }



        /// <summary>
        /// Registers new user
        /// </summary>
        /// <remarks>Registers a new user and authenticates it, generating a valid JWT</remarks>
        /// <param name="userDto">New user's data</param>
        [HttpPost("register", Name = nameof(Register))]
        [SwaggerResponse(StatusCodes.Status201Created, "User successfully registered and authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error registering user")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var registerResponse = await _authService.RegisterAsync(userDto);
            var authenticateResponse = await _authService.AuthenticateAsync(userDto);

            return CreatedAtRoute(nameof(UserController.GetUserById), new { authenticateResponse.User.Id }, authenticateResponse);
        }


        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <remarks>Authenticates an user, generating a valid JWT</remarks>
        /// <param name="userLogin">User's username and password</param>
        [HttpPost("authenticate", Name = nameof(Authenticate))]
        [SwaggerResponse(StatusCodes.Status200OK, "User authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> Authenticate(UserLoginDto userLogin)
        {
            var authResponse = await _authService.AuthenticateAsync(userLogin);

            return Ok(authResponse);
        }

        /// <summary>
        /// Easy registration for a new test user with roles assigned
        /// </summary>
        /// <remarks>
        /// Data is randomized.
        /// Default password: 'test'.
        ///
        /// Example:
        /// - Username: randomusername123
        /// - Password: test
        /// </remarks>
        /// <param name="roleId">Roles assigned to the test user</param>
        [HttpPost("register/testAccount", Name = nameof(RegisterTestAcc))]
        [SwaggerResponse(StatusCodes.Status201Created, "User successfully registered and authenticated", typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error registering user")]
        public async Task<IActionResult> RegisterTestAcc([FromQuery] List<RolesEnum> roleId)
        {           
            var authenticateResponse = await _testUserService.RegisterTestAcc(roleId);

            return CreatedAtRoute(nameof(UserController.GetUserById), new { authenticateResponse.User.Id }, authenticateResponse);
        }



        /// <summary>
        /// Easy authentication with a default test acc
        /// </summary>
        /// <param name="user">Test user to authenticate</param>
        [HttpPost("authenticate/testAccount/{user}", Name = nameof(AuthenticateTestUser))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        public async Task<IActionResult> AuthenticateTestUser(UserEnum user)
        {
            await _userService.ResetTestUsers();

            var testAdminLogin = TestAccountUsersLoginFactory.Generate(user);

            var authResponse = await _authService.AuthenticateAsync(testAdminLogin);
            
            return Ok(authResponse);
        }
    }
}