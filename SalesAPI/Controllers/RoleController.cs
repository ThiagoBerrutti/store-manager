using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllDtoAsync();
            return Ok(roles);
        }


        [HttpGet("{id:int}", Name = nameof(GetRoleById))]
        public async Task<ActionResult<RoleReadDto>> GetRoleById(int id)
        {
            var employee = await _roleService.GetDtoByIdAsync(id);
            return Ok(employee);
        }


        [HttpGet("{roleName}", Name = nameof(GetRoleByName))]
        public async Task<ActionResult<RoleReadDto>> GetRoleByName(string roleName)
        {
            var employee = await _roleService.GetDtoByNameAsync(roleName);
            return Ok(employee);
        }


        [HttpGet("{id}/users")]
        public async Task<ActionResult<UserViewModel>> GetUsersOnRole(int id)
        {
            var users = await _roleService.GetAllUsersOnRole(id);
            return Ok(users);
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> SearchRole(string name)
        {
            var employees = await _roleService.SearchByNameAsync(name);
            return Ok(employees);
        }


        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleWriteDto roleDto)
        {
            var roleOnRepo = await _roleService.CreateAsync(roleDto);

            return CreatedAtRoute(nameof(GetRoleById), new { roleOnRepo.Id }, roleOnRepo);
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteAsync(id);
            return Ok();
        }
    }
}