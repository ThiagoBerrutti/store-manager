using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Models;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        private readonly StockValidator _validator;

        public StockService(IStockRepository productStockRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _stockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _validator = new StockValidator();
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
                throw new DomainNotFoundException()
                    .SetTitle("Stock not found")
                    .SetDetail($"Stock for product [productId = {productId}] not found");
            }

            return stock;
        }


        public async Task<ProductStockReadDto> GetDtoByProductIdAsync(int productId)
        {
            var stock = await GetByProductIdAsync(productId);
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            return dto;
        }


        public async Task<ProductStock> GetByIdAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                throw new DomainNotFoundException()
                            .SetTitle("Stock not found")
                            .SetDetail($"Stock [Id = {id}] not found.");
            }

            return stock;
        }


        public async Task<ProductStockReadDto> GetDtoByIdAsync(int id)
        {
            var stock = await GetByIdAsync(id);
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            return dto;
        }


        public ProductStock CreateProductStock(Product product, int startingAmount = 0)
        {
            var productStock = new ProductStock(product, startingAmount);
            _stockRepository.Create(productStock);

            return productStock;
        }


        public async Task<ProductStockReadDto> UpdateAsync(int id, ProductStockWriteDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid stock data. See 'errors' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var psOnRepo = await GetByIdAsync(id);
            _mapper.Map(dto, psOnRepo);

            _stockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(psOnRepo);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> AddProductAmountAsync(int id, int amount)
        {
            if (amount <= 0)
            {
                throw new AppException()
                    .SetTitle("Error adding amount")
                    .SetDetail("Amount should be a positive number")
                    .SetInstance(StockInstancePath(id));
            }

            var productStock = await GetByIdAsync(id);
            if (productStock.Count >= 0 && (productStock.Count + amount < 0)) //overflow test
            {
                throw new AppException()
                    .SetTitle("Error adding amount")
                    .SetDetail("Stock's [Count] value will overflow")
                    .SetInstance(StockInstancePath(id));
            }

            productStock.Count += amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> RemoveProductAmountAsync(int id, int amount)
        {
            if (amount <= 0)
            {
                throw new AppException()
                    .SetTitle("Error adding amount")
                    .SetDetail("Amount should be a positive number")
                    .SetInstance(StockInstancePath(id));
            }

            var productStock = await GetByIdAsync(id);
            if (productStock.Count - amount < 0)
            {
                throw new AppException()
                    .SetTitle($"Not enough products available")
                    .SetDetail($"Only [{productStock.Count}] on stock")
                    .SetInstance(StockInstancePath(id));
            }

            productStock.Count -= amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }


        private string StockInstancePath(object id)
        {
            return _linkGenerator.GetPathByName(nameof(Controllers.StockController.GetStockById), new { id });
        }
    }
}