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
        private IEmployeeRepository _employeeRepository;
        private IEmployeePositionService _employeePositionService;
        private IEmployeeMapper _employeeMapper;
        private IUnitOfWork _unitOfWork;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IEmployeePositionService employeePositionService,
            IEmployeeMapper employeeMapper,
            IUnitOfWork unitOfWork
            )
        {
            _employeeRepository = employeeRepository;
            _employeePositionService = employeePositionService;
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
            var validPositionOnRepo = await _employeePositionService.GetByIdAsync(dto.PositionId); // throw if doesnt exist

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

        public async Task<IEnumerable<EmployeeReadDto>> SearchAsync(string name = "", int employeePositionId = 0)
        {
            Expression<Func<Employee, bool>> expression = e =>
                e.Name.Contains(name) &&
                (employeePositionId == 0 || e.PositionId == employeePositionId);

            var employeesOnRepo = await _employeeRepository.GetAllWhereAsync(expression);
            
            if (employeePositionId != 0)
            {
                employeesOnRepo = employeesOnRepo.Where(e => e.PositionId == employeePositionId);
            }

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

        public async Task<IEnumerable<EmployeeReadDto>> GetByPositionId(int employeePositionId)
        {
            var employees = await _employeeRepository.GetAllWhereAsync(e => e.PositionId == employeePositionId);
            var dtos = _employeeMapper.MapEntityToDtoList(employees);

            return dtos;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllWhere(int employeePositionId)
        {
            var employees = await _employeeRepository.GetAllWhereAsync(e => e.PositionId == employeePositionId);
            var dtos = _employeeMapper.MapEntityToDtoList(employees);

            return dtos;
        }

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