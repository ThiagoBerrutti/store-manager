using AutoMapper;
using Microsoft.AspNetCore.Routing;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Extensions;
using StoreAPI.Identity;
using StoreAPI.Infra;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Validations;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly RoleValidator _validator;
        private readonly RoleParametersValidator _roleParametersValidator;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _validator = new RoleValidator();
            _roleParametersValidator = new RoleParametersValidator();
        }


        public async Task<PaginatedList<RoleReadDto>> GetAllDtoPaginatedAsync(RoleParametersDto parameters)
        {
            var validationResult = _roleParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid query string parameters. Check '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            Expression<Func<Role, bool>> expression =
                r =>
                    r.Name.ToLower().Contains(parameters.Name.ToLower()) &&
                    r.Users.Select(u => u.Id)
                        .Where(id => parameters.UserId.Contains(id))
                        .Count() == parameters.UserId.Count();

            var result = await _roleRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);

            var dto = _mapper.Map<PaginatedList<Role>, PaginatedList<RoleReadDto>>(result);

            return dto;
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


        public async Task<RoleReadDto> CreateAsync(RoleWriteDto roleWriteDto)
        {
            var validationResult = _validator.Validate(roleWriteDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid role data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var role = _mapper.Map<Role>(roleWriteDto);
            var result = await _roleRepository.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException(result)
                    .SetTitle("Error creating role")
                    .SetDetail($"Role not created. See '{ExceptionWithProblemDetails.ErrorKey}' property for more details");
            }

            var appRole = await _roleRepository.GetByNameAsync(roleWriteDto.Name);
            var roleReturn = _mapper.Map<RoleReadDto>(appRole);

            return roleReturn;
        }


        public async Task DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);
            if (role.Name == AppConstants.Roles.Admin.Name ||
                role.Name == AppConstants.Roles.Manager.Name ||
                role.Name == AppConstants.Roles.Stock.Name ||
                role.Name == AppConstants.Roles.Seller.Name)
            {
                throw new AppException()
                    .SetTitle("Error deleting role")
                    .SetDetail($"Role '{role.Name}' cannot be deleted.")
                    .SetInstance(RoleInstance(id));
            }

            var result = await _roleRepository.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new IdentityException(result)
                    .SetTitle("Error deleting role")
                    .SetDetail($"Role not deleted. See '{ExceptionWithProblemDetails.ErrorKey}' property for more details");
            }
        }


        public async Task<PaginatedList<UserReadDto>> GetAllUsersOnRolePaginatedAsync(int id, QueryStringParameterDto parameters)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            var usersOnRole = role.Users;

            var paginatedUsers = usersOnRole
                    .OrderBy(u => u.FirstName)
                    .ThenBy(p => p.Id)
                    .ToPaginatedList(parameters.PageNumber, parameters.PageSize);

            var usersReadDtoPaginated = _mapper.Map<PaginatedList<UserReadDto>>(paginatedUsers);

            return usersReadDtoPaginated;
        }


        private string RoleInstance(object id)
        {
            return _linkGenerator.GetPathByName(nameof(Controllers.RoleController.GetRoleById), new { id });
        }
    }
}