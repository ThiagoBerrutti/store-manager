using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Dtos;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleReadDto> CreateAsync(RoleWriteDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {                
                throw new IdentityException("Role couldn't be created", result.Errors);
            }

            var appRole = await _roleManager.FindByNameAsync(dto.Name);
            var roleReturn = _mapper.Map<RoleReadDto>(appRole);

            return roleReturn;
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                throw new DomainNotFoundException($"Role [Id = {id}] not found.");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException("Role couldn't be deleted", result.Errors);
            }
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var rolesDto = _mapper.Map<IEnumerable<RoleReadDto>>(roles);

            return rolesDto;
        }

        public async Task<RoleReadDto> GetByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                throw new DomainNotFoundException($"Role [Id = {id}] not found.");
            }

            var roleDto = _mapper.Map<RoleReadDto>(role);

            return roleDto;
        }

        public async Task<RoleReadDto> GetByName(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
            {
                throw new DomainNotFoundException($"Role [Name = {name}] not found.");
            }

            var roleDto = _mapper.Map<RoleReadDto>(role);

            return roleDto;
        }
    }
}