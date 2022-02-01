using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using SalesAPI.Identity.Services;
using SalesAPI.Models;
using SalesAPI.Services;
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(RoleWriteDto roleDto)
        {
            var roleOnRepo = await _roleService.CreateAsync(roleDto);

            return CreatedAtRoute(nameof(GetByIdAsync), new { roleOnRepo.Id }, roleOnRepo);
        }

        [Authorize(Roles = "admin,manager")]
        [HttpGet("{id:int}", Name = "GetByIdAsync")]
        public async Task<ActionResult<Role>> GetByIdAsync(int id)
        {
            var employee = await _roleService.GetByIdAsync(id);
            return Ok(employee);
        }

        [Authorize(Roles = "admin,manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [Authorize(Roles = "admin,manager")]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> Search(string name)
        {
            var employees = await _roleService.GetByName(name);
            return Ok(employees);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int ide)
        {
            await _roleService.DeleteAsync(ide);
            return Ok();
        }
    }
}