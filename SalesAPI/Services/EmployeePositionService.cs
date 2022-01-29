using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class EmployeePositionService : IEmployeePositionService
    {
        private readonly IEmployeePositionRepository _employeePositionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeePositionService(IEmployeePositionRepository employeePositionRepository, IUnitOfWork unitOfWork)
        {
            _employeePositionRepository = employeePositionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeePosition> CreateAsync(EmployeePositionWriteDto dto)
        {
            var employeePos = new EmployeePosition { Name = dto.Name };
            _employeePositionRepository.Add(employeePos);

            await _unitOfWork.CompleteAsync();

            return employeePos;
        }

        public async Task CreateAsync(string name)
        {
            var employeePos = new EmployeePosition { Name = name };
            _employeePositionRepository.Add(employeePos);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employeePositionOnRepo = await GetByIdAsync(id);
            _employeePositionRepository.Delete(employeePositionOnRepo);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<EmployeePosition>> GetAllAsync()
        {
            return await _employeePositionRepository.GetAllAsync();
        }

        public async Task<EmployeePosition> GetByIdAsync(int id)
        {
            var entity = await _employeePositionRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"No employee position [Id = {id}] found.");
            }

            return entity;
        }

        public async Task<IEnumerable<EmployeePosition>> GetByName(string name)
        {
            return await _employeePositionRepository.GetAllWhereAsync(e => e.Name.Contains(name));
        }

        public async Task UpdateNameAsync(int id, string name)
        {
            var employeePositionOnRepo = await GetByIdAsync(id);

            employeePositionOnRepo.Name = name;
            _employeePositionRepository.UpdateAsync(employeePositionOnRepo);

            await _unitOfWork.CompleteAsync();
        }
    }
}