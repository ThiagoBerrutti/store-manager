using SalesAPI.Models;
using SalesAPI.Dtos;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public interface IEmployeeMapper
    {
        public Employee MapDtoToEntity(EmployeeWriteDto dto);
        public Employee MapDtoToEntity(EmployeeWriteDto dto, Employee mapInto);

        public EmployeeReadDto MapEntityToDto(Employee employee);
        public IEnumerable<EmployeeReadDto> MapEntityToDtoList(IEnumerable<Employee> employeeList);

    }
}