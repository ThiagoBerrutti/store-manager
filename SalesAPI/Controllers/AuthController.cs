using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Identity;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
    }
}