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
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IStockRepository productStockRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _stockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<ProductStockReadDto>> GetAllDtoAsync()
        {
            var stockList = await _stockRepository.GetAll();
            var dtoList = _mapper.Map<IEnumerable<ProductStock>, IEnumerable<ProductStockReadDto>>(stockList);

            return dtoList;
        }


        public async Task<ProductStock> GetByProductIdAsync(int productId)
        {
            var stock = await _stockRepository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                throw new DomainNotFoundException($"Stock for product [productId = {productId}] not found.");
            }

            return stock;
        }


        public async Task<ProductStockReadDto> GetDtoByProductIdAsync(int productId)
        {
            var stock = await GetByProductIdAsync(productId);
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            return dto;
        }


        public ProductStock CreateProductStock(Product product, int startingAmount = 0)
        {
            var productStock = new ProductStock(product, startingAmount);
            _stockRepository.Create(productStock);

            return productStock;
        }


        public async Task DeleteAsync(int productId)
        {
            var stockOnRepo = await GetByProductIdAsync(productId);

            stockOnRepo.Count = 0;

            await _unitOfWork.CompleteAsync();
        }


        public async Task<ProductStockReadDto> AddProductAmountAsync(int productId, int amount)
        {
            if (amount <= 0)
            {
                throw new ApplicationException("Amount should be a positive number.");
            }

            var productStock = await GetByProductIdAsync(productId);            

            productStock.Count += amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> RemoveProductAmountAsync(int productId, int amount)
        {
            if (amount <= 0)
            {
                throw new ApplicationException("Amount should be a positive number.");
            }

            var productStock = await GetByProductIdAsync(productId);
            
            if (productStock.Count - amount < 0)
            {
                throw new StockException($"Not enough products available. Only [{productStock.Count}] on stock.");
            }

            productStock.Count -= amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }
        

        public async Task<ProductStockReadDto> UpdateAsync(int productId, ProductStockWriteDto dto)
        { 
            var psOnRepo = await GetByProductIdAsync(productId);
            _mapper.Map(dto, psOnRepo);

            _stockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(psOnRepo);
            return productStockDto;
        }
    }
}