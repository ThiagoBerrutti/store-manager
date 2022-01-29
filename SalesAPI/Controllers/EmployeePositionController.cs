using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Models;
using SalesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/eeeee")]
    public class EmployeePositionController : ControllerBase
    {
        private IEmployeePositionService _employeePositionService;

        public EmployeePositionController(IEmployeePositionService employeePositionService)
        {
            _employeePositionService = employeePositionService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(EmployeePositionWriteDto employeePositionDto)
        {
            var employeePositionOnRepo = await _employeePositionService.CreateAsync(employeePositionDto);
            return CreatedAtRoute(nameof(GetByIdAsync), new { employeePositionOnRepo.Id }, employeePositionOnRepo); 
        }

        [HttpGet("{id:int}", Name = "GetByIdAsync")]
        public async Task<ActionResult<EmployeePosition>> GetByIdAsync(int id)
        {
            var employee = await _employeePositionService.GetByIdAsync(id);
            return Ok(employee);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> GetAll()
        {
            var employees = await _employeePositionService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> Search(string name)
        {
            var employees = await _employeePositionService.GetByName(name);
            return Ok(employees);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int ide)
        {
            await _employeePositionService.DeleteAsync(ide);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateName(int id, string name)
        {
            await _employeePositionService.UpdateNameAsync(id, name);
            return Ok();
        }
    }
}
