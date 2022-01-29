using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeReadDto>> GetById([FromRoute] int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeWriteDto newEmployee)
        {
            await _employeeService.CreateAsync(newEmployee);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> Search(int positionId = 0, string name = "")
        {
            var employees = await _employeeService.SearchAsync(name, positionId);
            return Ok(employees);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, EmployeeWriteDto changedEmployee)
        {
            await _employeeService.UpdateAsync(id, changedEmployee);
            return Ok();
        }
    }
}