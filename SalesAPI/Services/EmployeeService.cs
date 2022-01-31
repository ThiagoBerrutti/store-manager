using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Mapper;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeMapper _employeeMapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IEmployeeMapper employeeMapper,
            IUnitOfWork unitOfWork
            )
        {
            _employeeRepository = employeeRepository;
            _employeeMapper = employeeMapper;
            _unitOfWork = unitOfWork;
        }

        private async Task<Employee> GetEntityByIdAsync(int id)
        {
            var employeeOnRepo = await _employeeRepository.GetByIdAsync(id);
            if (employeeOnRepo == null)
            {
                throw new EntityNotFoundException($"No employee[Id = {id}] found.");
            }

            return employeeOnRepo;
        }

        public async Task CreateAsync(EmployeeWriteDto dto)
        {
            //var roleOnRepo = await _roleService.GetByIdAsync(dto.RoleId); // throw if doesnt exist

            var employee = _employeeMapper.MapDtoToEntity(dto);
            _employeeRepository.CreateAsync(employee);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllAsync()
        {
            var employeesOnRepo = await _employeeRepository.GetAllAsync();
            var result = _employeeMapper.MapEntityToDtoList(employeesOnRepo);

            return result;
        }

        public async Task<IEnumerable<EmployeeReadDto>> SearchAsync(string name = "")
        {
            Expression<Func<Employee, bool>> expression = e =>
                e.Name.Contains(name);
                //&& (roleId == 0 || e.RoleId == roleId);

            var employeesOnRepo = await _employeeRepository.GetAllWhereAsync(expression);
            
            //if (roleId != 0)
            //{
            //    employeesOnRepo = employeesOnRepo.Where(e => e.RoleId == roleId);
            //}

            var result = _employeeMapper.MapEntityToDtoList(employeesOnRepo);

            return result;
        }

        public async Task<EmployeeReadDto> GetByIdAsync(int id)
        {
            var employeeOnRepo = await GetEntityByIdAsync(id);

            var dto = _employeeMapper.MapEntityToDto(employeeOnRepo);
            return dto;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetByNameAsync(string name)
        {
            var employees = await _employeeRepository.GetAllWhereAsync(e => e.Name.Contains(name));
            var dtos = _employeeMapper.MapEntityToDtoList(employees);

            return dtos;
        }

        //public async Task<IEnumerable<EmployeeReadDto>> GetByRoleId(int roleId)
        //{
        //    var employees = await _employeeRepository.GetAllWhereAsync(e => e.RoleId == roleId);
        //    var dtos = _employeeMapper.MapEntityToDtoList(employees);

        //    return dtos;
        //}

        //public async Task<IEnumerable<EmployeeReadDto>> GetAllWhere(int roleId)
        //{
        //    var employees = await _employeeRepository.GetAllWhereAsync(e => e.RoleId == roleId);
        //    var dtos = _employeeMapper.MapEntityToDtoList(employees);

        //    return dtos;
        //}

        public async Task UpdateAsync(int id, EmployeeWriteDto employeeUpdate)
        {
            var employeeOnRepo = await GetEntityByIdAsync(id);

            _employeeMapper.MapDtoToEntity(employeeUpdate, employeeOnRepo);
            _employeeRepository.UpdateAsync(employeeOnRepo);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employeeOnRepo = await GetEntityByIdAsync(id);
            _employeeRepository.Delete(employeeOnRepo);

            await _unitOfWork.CompleteAsync();
        }
    }
}