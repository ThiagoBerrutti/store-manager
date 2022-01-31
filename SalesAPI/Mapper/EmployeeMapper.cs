using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public class EmployeeMapper : IEmployeeMapper
    {
        public Employee MapDtoToEntity(EmployeeWriteDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                BaseSalary = dto.BaseSalary,
            };
        }

        public Employee MapDtoToEntity(EmployeeWriteDto dto, Employee mapInto)
        {
            mapInto.Name = dto.Name;
            mapInto.BaseSalary = dto.BaseSalary;

            return new Employee
            {
                Name = dto.Name,
                BaseSalary = dto.BaseSalary,
                
            };
        }

        public IEnumerable<EmployeeReadDto> MapEntityToDtoList(IEnumerable<Employee> employeeList)
        {
            var result = new List<EmployeeReadDto>();

            foreach(Employee e in employeeList)
            {
                var dto = MapEntityToDto(e);
                result.Add(dto);
            }

            return result;
        }


        public EmployeeReadDto MapEntityToDto(Employee employee)
        {
            return new EmployeeReadDto
            {
                Id = employee.Id,
                Name = employee.Name,
                BaseSalary = employee.BaseSalary
            };
        }
    }
}