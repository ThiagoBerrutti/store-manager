using AutoMapper;
using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Identity;
using SalesAPI.Persistence.Repositories;
using SalesAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        private readonly RoleValidator _validator;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _validator = new RoleValidator();
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
                throw new IdentityNotFoundException()
                    .SetTitle("Role not found")
                    .SetDetail($"Role [Id = {id}] not found.");
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
                throw new IdentityNotFoundException()
                    .SetTitle("Role not found.")
                    .SetDetail($"Role '{name}' not found.");
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


        public async Task<RoleReadDto> CreateAsync(RoleWriteDto roleWriteDto)
        {
            var validationResult = _validator.Validate(roleWriteDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid role data. See 'errors' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var role = _mapper.Map<Role>(roleWriteDto);
            var result = await _roleRepository.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error creating role")
                    .SetDetail("Role not created. See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description));
            }

            var appRole = await _roleRepository.GetByNameAsync(roleWriteDto.Name);
            var roleReturn = _mapper.Map<RoleReadDto>(appRole);

            return roleReturn;
        }


        public async Task DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);

            var result = await _roleRepository.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException()
                    .SetTitle("Error deleting role")
                    .SetDetail("Role not deleted. See 'errors' property for more details")
                    .SetErrors(result.Errors.Select(e => e.Description));

            }
        }


        public async Task<IEnumerable<UserViewModel>> GetAllUsersOnRole(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(role.Users);

            return usersViewModel;
        }
    }
}