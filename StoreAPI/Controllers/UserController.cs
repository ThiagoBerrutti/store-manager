using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Services;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// User related operations
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/users")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Finds all users, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserParametersDto parameters)
        {           
            var result = await _userService.GetAllDtoPaginatedAsync(parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }



        /// <summary>
        /// Finds an user by Id
        /// </summary>
        /// <remarks>Returns a single product</remarks>
        /// <param name="id">Product Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetDtoByIdAsync(id);

            return Ok(result);
        }



        /// <summary>
        /// Returns current user
        /// </summary>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userService.GetCurrentUserDtoAsync();

            return Ok(result);
        }


        /// <summary>
        /// Finds all roles an user is assigned, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        /// <param name="id" example="1,2,3">User Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRolesFromUser(int id, [FromQuery] QueryStringParameterDto parameters)
        {
            var result = await _userService.GetAllRolesFromUser(id, parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string userName, UserUpdateDto userUpdate)
        {
            var currentUserClaims = HttpContext.User;
            var currentUserName = currentUserClaims.FindFirst(ClaimTypes.Name).Value;
            var isCurrentUser = currentUserName.ToUpper() == userName.ToUpper();

            if (!(currentUserClaims.IsInRole("Administrator") || currentUserClaims.IsInRole("Manager") || isCurrentUser))
            {
                return new UnauthorizedResult();
            }

            var result = await _userService.UpdateUserAsync(userName, userUpdate);

            return Ok(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDto passwords)
        {
            await _userService.ChangePasswordAsync(id, passwords);

            return Ok();
        }


        [HttpPut("current/password")]
        public async Task<IActionResult> ChangeCurrentUserPassword(ChangePasswordDto passwords)
        {
            await _userService.ChangeCurrentUserPasswordAsync(passwords);

            return Ok();
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}/password")]
        public async Task<IActionResult> ResetPassword(int id, string newPassword = "")
        {
            await _userService.ResetPasswordAsync(id, newPassword);

            return Ok();
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}/roles/add/{roleId}")]
        public async Task<IActionResult> AddUserToRole(int id, int roleId)
        {
            var user = await _userService.AddToRoleAsync(id, roleId);

            return Ok(user);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}/roles/remove/{roleId}")]
        public async Task<IActionResult> RemoveFromRole(int id, int roleId)
        {
            var user = await _userService.RemoveFromRoleAsync(id, roleId);

            return Ok(user);
        }
    }
}