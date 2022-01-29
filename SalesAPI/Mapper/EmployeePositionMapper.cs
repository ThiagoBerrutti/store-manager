using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public class EmployeePositionMapper
    {
        public EmployeePosition MapDtoToEntity(EmployeePositionWriteDto dto)
        {
            return new EmployeePosition
            {
                Name = dto.Name,
            };
        }

        public EmployeePosition MapDtoToEntity(EmployeePositionWriteDto dto, EmployeePosition mapInto)
        {
            mapInto.Name = dto.Name;           

            return new EmployeePosition
            {
                Name = dto.Name
            };
        }

        public IEnumerable<EmployeePositionReadDto> MapEntityToDtoList(IEnumerable<EmployeePosition> employeeList)
        {
            var result = new List<EmployeePositionReadDto>();

            foreach (EmployeePosition e in employeeList)
            {
                var dto = MapEntityToDto(e);
                result.Add(dto);
            }

            return result;
        }

        public EmployeePositionReadDto MapEntityToDto(EmployeePosition employee)
        {
            return new EmployeePositionReadDto
            {
                Id = employee.Id,
                Name = employee.Name
            };
        }
    }
}