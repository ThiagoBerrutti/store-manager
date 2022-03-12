using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Infra;
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
    /// Operations about roles
    /// </summary>
    [Authorize(Roles = "Administrator,Manager")]
    [ApiController]
    [Route("api/v1/roles")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(void))]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        /// <summary>
        /// Finds all roles, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<RoleReadDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [HttpGet(Name = nameof(GetAllRolesPaginated))]
        public async Task<IActionResult> GetAllRolesPaginated([FromQuery] RoleParametersDto parameters)
        {
            var response = await _roleService.GetAllDtoPaginatedAsync(parameters);
            if (!response.Success)
            {
                return new ProblemDetailsObjectResult(response.Error);
            }

            var page = response.Data;
            var result = page.Items;
            var metadata = page.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result);
        }


        /// <summary>
        /// Finds a role by Id
        /// </summary>
        /// <remarks>Returns a single role</remarks>
        /// <param name="id">The role's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Role found", typeof(RoleReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [HttpGet("{id:int}", Name = nameof(GetRoleById))]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleService.GetDtoByIdAsync(id);
            if (!response.Success)
            {
                return new ProblemDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }


        /// <summary>
        /// Finds a role by name
        /// </summary>
        /// <remarks>Name must be an exact match, but it is not case-sensitive</remarks>
        /// <param name="roleName">The role's name</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Role found", typeof(RoleReadDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [HttpGet("{roleName}", Name = nameof(GetRoleByName))]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            var response = await _roleService.GetDtoByNameAsync(roleName);
            if (!response.Success)
            {
                return new ProblemDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return Ok(result);
        }

        /// <summary>
        /// Finds all users on a role, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        /// <param name="id">Role's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<RoleReadDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "string", Descriptions.XPaginationDescription)]
        [HttpGet("{id}/users", Name = nameof(GetUsersOnRole))]
        public async Task<IActionResult> GetUsersOnRole(int id, [FromQuery] QueryStringParameterDto parameters)
        {
            var response = await _roleService.GetAllUsersOnRolePaginatedAsync(id, parameters);
            if (!response.Success)
            {
                return new ProblemDetailsObjectResult(response.Error);
            }

            var page = response.Data;
            var result = page.Items;
            var metadata = page.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result);
        }


        /// <summary>
        /// Create a new role
        /// </summary>
        /// <remarks>Role names are unique</remarks>
        /// <param name="roleDto">Role object to be created</param>
        [SwaggerResponse(StatusCodes.Status201Created, "Role created", typeof(RoleReadDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(CreateRole))]
        public async Task<IActionResult> CreateRole(RoleWriteDto roleDto)
        {
            var response = await _roleService.CreateAsync(roleDto);
            if (!response.Success)
            {
                return new ProblemDetailsObjectResult(response.Error);
            }

            var result = response.Data;

            return CreatedAtRoute(nameof(GetRoleById), new { result.Id }, result);
        }


        /// <summary>
        /// Delete an existing role
        /// </summary>
        /// <param name="id">The role Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Role deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}", Name = nameof(DeleteRole))]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteAsync(id);
            if (!response.Success)
            {
                return new ProblemDetailsObjectResult(response.Error);
            }

            return NoContent();
        }
    }
}