using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public class RoleMapper
    {
        public Role MapDtoToEntity(RoleWriteDto dto)
        {
            return new Role
            {
                Name = dto.Name,
            };
        }

        public Role MapDtoToEntity(RoleWriteDto dto, Role mapInto)
        {
            mapInto.Name = dto.Name;           

            return new Role
            {
                Name = dto.Name
            };
        }

        public IEnumerable<RoleReadDto> MapEntityToDtoList(IEnumerable<Role> employeeList)
        {
            var result = new List<RoleReadDto>();

            foreach (Role e in employeeList)
            {
                var dto = MapEntityToDto(e);
                result.Add(dto);
            }

            return result;
        }

        public RoleReadDto MapEntityToDto(Role employee)
        {
            return new RoleReadDto
            {
                Id = employee.Id,
                Name = employee.Name
            };
        }
    }
}