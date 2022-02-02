using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using SalesAPI.Identity.Services;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [Produces("text/json")]
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly SignInManager<User> _signInManager;
        //private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserController(SignInManager<User> signInManager, 
                                UserManager<User> userManager,
                                IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            //_signInManager = signInManager;
            //_userManager = userManager;
            _tokenService = tokenService;
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllDtoAsync();

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetDtoByIdAsync(id);

            return Ok(result);
        }

        
        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userService.GetDtoCurrentUserAsync();

            return Ok(result);
        }


        [Authorize(Roles = "Administrator,Manager")]        
        [HttpGet("name", Name = "GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var result = await _userService.GetDtoByUserNameAsync(userName);

            return Ok(result);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var registerResponse = await _userService.RegisterAsync(userDto);
            var authenticateResponse = await _userService.AuthenticateAsync(userDto);

            return Created(nameof(GetUserByUserName), registerResponse);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            var authResponse = await _userService.AuthenticateAsync(userLogin);

            return Ok(authResponse);
        }
        

        [Authorize(Roles = "Administrator")]
        [HttpPut("name/changePassword")]
        public async Task<IActionResult> ChangePassword(string userName, [FromBody] ChangePasswordsDto passwords)
        {
            await _userService.ChangePasswordAsync(userName, passwords.CurrentPassword, passwords.NewPassword);

            return Ok();
        }

        [HttpPut("current/changePassword")]
        public async Task<IActionResult> ChangeCurrentUserPassword([FromBody] ChangePasswordsDto passwords)
        {
            await _userService.ChangeCurrentUserPasswordAsync(passwords.CurrentPassword, passwords.NewPassword);

            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("name/ResetPassword")]
        public async Task<IActionResult> ResetPassword(string userName)
        {
            var newPassword = userName;
            await _userService.ResetPasswordAsync(userName, newPassword);

            return Ok();
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id:int}/roles")]
        public async Task<IActionResult> AddUserToRole(int id, int roleId)
        {
            var user = await _userService.AddToRoleAsync(id,roleId);

            return Ok(user);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{id:int}/roles")]
        public async Task<IActionResult> RemoveFromRole(int id, int roleId)
        {
            var user = await _userService.RemoveFromRoleAsync(id, roleId);

            return Ok(user);
        }
    }
}