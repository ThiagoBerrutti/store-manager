using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using SalesAPI.Identity.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetAll()
        {
            var roles = await _roleService.GetAllDtoAsync();
            return Ok(roles);
        }
        
        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<RoleReadDto>> GetById(int id)
        {
            var employee = await _roleService.GetDtoByIdAsync(id);
            return Ok(employee);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("name", Name = "GetByName")]
        public async Task<ActionResult<RoleReadDto>> GetByName(string roleName)
        {
            var employee = await _roleService.GetDtoByNameAsync(roleName);
            return Ok(employee);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("{id:int}/users")]
        public async Task<ActionResult<UserViewModel>> GetUsersOnRole(int id)
        {
            var users = await _roleService.GetAllUsersOnRole(id);
            return Ok(users);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> Search(string name)
        {
            var employees = await _roleService.SearchByNameAsync(name);
            return Ok(employees);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<ActionResult> Create(RoleWriteDto roleDto)
        {
            var roleOnRepo = await _roleService.CreateAsync(roleDto);

            return CreatedAtRoute(nameof(GetById), new { roleOnRepo.Id }, roleOnRepo);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int ide)
        {
            await _roleService.DeleteAsync(ide);
            return Ok();
        }

        
    }
}