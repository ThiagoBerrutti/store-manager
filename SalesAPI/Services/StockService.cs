using AutoMapper;
using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class StockService : IStockService
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IProductStockRepository productStockRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<ProductStockReadDto>> GetAllDtoAsync()
        {
            var stockList = await _productStockRepository.GetAll();
            var dtoList = _mapper.Map<IEnumerable<ProductStock>, IEnumerable<ProductStockReadDto>>(stockList);

            return dtoList;
        }


        public async Task<ProductStockReadDto> GetDtoByProductId(int productId)
        {
            var productStock = await _productStockRepository.GetByProductIdAsync(productId);
            if (productStock == null)
            {
                throw new DomainNotFoundException($"Stock for product [productId = {productId}] not found.");
            }

            var dto = _mapper.Map<ProductStockReadDto>(productStock);

            return dto;
        }


        public ProductStock CreateProductStock(Product product, int startingAmount = 0)
        {
            var productStock = new ProductStock(product, startingAmount);
            _productStockRepository.Create(productStock);

            return productStock;
        }


        public async Task Delete(int id)
        {
            var stockOnRepo = await _productStockRepository.GetByIdAsync(id);
            if (stockOnRepo == null)
            {
                throw new DomainNotFoundException($"Stock [Id = {id}] not found.");
            }

            stockOnRepo.Count = 0;

            await _unitOfWork.CompleteAsync();
        }


        public async Task<ProductStockReadDto> AddProductAmount(int id, int amount)
        {
            if (amount <= 0)
            {
                throw new ApplicationException("Amount should be a positive number.");
            }

            var productStock = await _productStockRepository.GetByIdAsync(id);
            if (productStock == null)
            {
                throw new DomainNotFoundException($"Stock [Id = {id}] not found.");
            }

            productStock.Count += amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> RemoveProductAmount(int id, int amount)
        {
            if (amount <= 0)
            {
                throw new ApplicationException("Amount should be a positive number.");
            }

            var productStock = await _productStockRepository.GetByIdAsync(id);
            if (productStock == null)
            {
                throw new DomainNotFoundException($"Stock [Id = {id}] not found.");
            }

            if (productStock.Count - amount < 0)
            {
                throw new StockException($"Not enough products available. Only [{productStock.Count}] on stock.");
            }

            productStock.Count -= amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }
        

        public async Task<ProductStockReadDto> Update(int productId, ProductStockWriteDto dto)
        { 
            var psOnRepo = await _productStockRepository.GetByProductIdAsync(productId);
            _mapper.Map(dto, psOnRepo);

            _productStockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(psOnRepo);
            return productStockDto;
        }
    }
}