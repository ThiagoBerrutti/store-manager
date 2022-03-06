using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Services;
using StoreAPI.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
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
    [ProducesErrorResponseType(typeof(void))]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<UserReadDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [HttpGet(Name = nameof(GetAllUsersPaginated))]
        public async Task<IActionResult> GetAllUsersPaginated([FromQuery] UserParametersDto parameters)
        {
            var result = await _userService.GetAllDtoPaginatedAsync(parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }


        /// <summary>
        /// Finds an user by Id
        /// </summary>
        /// <remarks>Returns a single user with more details</remarks>
        /// <param name="id">User Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserDetailedReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetDtoByIdAsync(id);

            return Ok(result);
        }



        /// <summary>
        /// Returns current user
        /// </summary>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserDetailedReadDto))]
        [HttpGet("current", Name = nameof(GetCurrentUser))]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userService.GetCurrentUserDtoAsync();

            return Ok(result);
        }


        /// <summary>
        /// Finds all the roles that are assigned to a user
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result pagination values</param>
        /// <param name="id">User Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<RoleReadDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [HttpGet("{id}/roles", Name = nameof(GetRolesFromUser))]
        public async Task<IActionResult> GetRolesFromUser(int id, [FromQuery] QueryStringParameterDto parameters)
        {
            var result = await _userService.GetAllRolesFromUserPaginatedAsync(id, parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }


        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="userUpdate">User object with updated data</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "User updated", typeof(UserReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [HttpPut(Name = nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdate)
        {
            var result = await _userService.UpdateUserAsync(id, userUpdate);

            return Ok(result);
        }

        /// <summary>
        /// Updates the current user
        /// </summary>
        /// <param name="userUpdate">User object with updated data</param>
        [SwaggerResponse(StatusCodes.Status200OK, "User updated", typeof(UserReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [HttpPut("current", Name = nameof(UpdateCurrentUser))]
        public async Task<IActionResult> UpdateCurrentUser(UserUpdateDto userUpdate)
        {
            var currentUser = await _userService.GetCurrentUserDtoAsync();
            var currentUserUpdated = await _userService.UpdateUserAsync(currentUser.Id, userUpdate);

            return Ok(currentUserUpdated);
        }




        /// <summary>
        /// Forcefully change a user password.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="passwords">Passwords object with old and new values</param>
        [Authorize(Roles = "Administrator")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [HttpPut("{id}/password", Name = nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDto passwords)
        {
            await _userService.ChangePasswordAsync(id, passwords);

            return Ok();
        }



        /// <summary>
        /// Change the authenticated user password
        /// </summary>
        /// <remarks>Change your own password</remarks>
        /// <param name="passwords">Passwords object with old and new values</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPut("current/password", Name = nameof(ChangeCurrentUserPassword))]
        public async Task<IActionResult> ChangeCurrentUserPassword(ChangePasswordDto passwords)
        {
            await _userService.ChangeCurrentUserPasswordAsync(passwords);

            return Ok();
        }


        /// <summary>
        /// Forcefully resets an user password
        /// </summary>
        /// <remarks>When an user forgot a password and can't recover it, an admin can reset it</remarks>
        /// <param example="123" name="id">User Id</param>
        /// <param name="newPassword">If included, sets the new user password. If ignored, sets the username as password.
        /// Example:
        /// <details>
        /// <summary>Show examples</summary>
        ///
        ///
        /// <h4>Original user</h4>
        ///     <ul>
        ///         <li>Username: johndoe01</li>
        ///         <li>Password: password567</li>
        ///     </ul>
        ///
        /// <h4>After use with 'newPassword: ***abc123***'</h4>
        /// <ul>
        ///     <li>Username: johndoe01</li>
        ///     <li>Password: ***abc123*** <i>(Old: password567)</i></li>
        /// </ul>
        ///
        /// <h4>After use, but empty or no 'newPassword' field:</h4>
        /// <ul>
        ///     <li>Username: ***johndoe01***</li>
        ///     <li>Password: ***johndoe01*** <i>(Old: password567)</i></li>
        /// </ul>
        /// </details>
        /// </param>
        [Authorize(Roles = "Administrator")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password reset")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [HttpDelete("{id}/password", Name = nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] string newPassword = "")
        {
            await _userService.ResetPasswordAsync(id, newPassword);

            return Ok();
        }


        /// <summary>
        /// Assigns a role to an user
        /// </summary>
        /// <param name="id">Id of the role to add</param>
        /// <param name="roleId">User Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "User added to role", typeof(UserReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User or role not found")]
        [HttpPut("{id}/roles/add/{roleId}", Name = nameof(AddUserToRole))]
        public async Task<IActionResult> AddUserToRole(int id, int roleId)
        {
            var user = await _userService.AddToRoleAsync(id, roleId);

            return Ok(user);
        }

        /// <summary>
        /// Dismiss user from a role
        /// </summary>
        /// <param name="roleId">Id of the role to remove</param>
        /// <param name="id">User Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "User removed from role", typeof(UserReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User or role not found")]
        [HttpPut("{id}/roles/remove/{roleId}", Name = nameof(RemoveFromRole))]
        public async Task<IActionResult> RemoveFromRole(int id, int roleId)
        {
            var user = await _userService.RemoveFromRoleAsync(id, roleId);

            return Ok(user);
        }
    }
}