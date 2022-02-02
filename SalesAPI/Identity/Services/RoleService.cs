using AutoMapper;
using SalesAPI.Dtos;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllDtoAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            var rolesDto = _mapper.Map<IEnumerable<RoleReadDto>>(roles);

            return rolesDto;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new DomainNotFoundException($"Role [Id = {id}] not found.");
            };

            return role;
        }

        public async Task<RoleReadDto> GetDtoByIdAsync(int id)
        {
            var role = await GetByIdAsync(id);
            var roleDto = _mapper.Map<RoleReadDto>(role);

            return roleDto;
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            var role = await _roleRepository.GetByNameAsync(name);
            if (role == null)
            {
                throw new DomainNotFoundException($"Role ['{name}'] not found.");
            }

            return role;
        }

        public async Task<RoleReadDto> GetDtoByNameAsync(string name)
        {
            var role = await GetByNameAsync(name);
            var roleDto = _mapper.Map<RoleReadDto>(role);

            return roleDto;
        }

        public async Task<IEnumerable<RoleReadDto>> SearchByNameAsync(string name)
        {
            var roles = await _roleRepository.SearchByNameAsync(name);
            var rolesDto = _mapper.Map<IEnumerable<RoleReadDto>>(roles);

            return rolesDto;
        }

        public async Task<RoleReadDto> CreateAsync(RoleWriteDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            var result = await _roleRepository.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException("Role couldn't be created", result.Errors);
            }

            var appRole = await _roleRepository.GetByNameAsync(dto.Name);
            var roleReturn = _mapper.Map<RoleReadDto>(appRole);

            return roleReturn;
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new DomainNotFoundException($"Role [Id = {id}] not found.");
            }

            var result = await _roleRepository.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException("Role couldn't be deleted", result.Errors);
            }
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersOnRole(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(role.Users);

            return usersViewModel;
        }

        //public async Task<UserViewModel> AddUserToRoleAsync(int id, int userId)
        //{
        //    var user = await _userService.GetByIdAsync(userId);
        //    var role = await GetByIdAsync(id);

        //    var hasRole = user.Roles.Contains(role);
        //    if (hasRole)
        //    {
        //        throw new IdentityException($"User already assigned to role ['{role.Name}'].");
        //    }

        //    var result = await _roleRepository.AddUserToRoleAsync(user, role.Name);
        //    if (!result.Succeeded)
        //    {
        //        throw new IdentityException($"Error adding user ['{user.UserName}'] to role ['{role.Name}'] .", result.Errors);
        //    }

        //    var userWithRole = await _userService.GetByUserNameAsync(user.UserName);
        //    var userModel = _mapper.Map<UserViewModel>(userWithRole);

        //    return userModel;
        //}

        //public async Task<UserViewModel> RemoveUserFromRoleAsync(int id, int userId)
        //{
        //    var user = await _userService.GetByIdAsync(userId);
        //    var role = await GetByIdAsync(id);

        //    var hasRole = user.Roles.Contains(role);
        //    if (!hasRole)
        //    {
        //        throw new IdentityException($"User not assigned to role ['{role.Name}'].");
        //    }

        //    var result = await _roleRepository.RemoveFromRoleAsync(user, role.Name);
        //    if (!result.Succeeded)
        //    {
        //        throw new IdentityException($"Error removing user ['{user.UserName}'] to role ['{role.Name}'] .", result.Errors);
        //    }

        //    var userModel = _mapper.Map<UserViewModel>(user);
        //    return userModel;
        //}

        //users on role R
    }
}