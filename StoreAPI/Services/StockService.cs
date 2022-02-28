using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StoreAPI.Domain;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Validations;
using System;
using System.Linq.Expressions;
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
        private readonly StockParametersValidator _stockParametersValidator;

        public StockService(IStockRepository productStockRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _stockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _validator = new StockValidator();
            _stockParametersValidator = new StockParametersValidator();
        }


        public async Task<PaginatedList<ProductStockReadDto>> GetAllDtoPaginatedAsync(StockParametersDto parameters)
        {
            var validationResult = _stockParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid query string parameters. Check '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            Expression<Func<ProductStock, bool>> expression =
                s =>
                    s.Product.Name.ToLower().Contains(parameters.ProductName.ToLower()) &&
                    s.Quantity >= parameters.QuantityMin &&
                    s.Quantity <= parameters.QuantityMax;


            var result = await _stockRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);

            var dto = _mapper.Map<PaginatedList<ProductStock>, PaginatedList<ProductStockReadDto>>(result);

            return dto;
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


        public ProductStock CreateProductStock(Product product, int startingQuantity = 0)
        {
            var productStock = new ProductStock(product, startingQuantity);
            _stockRepository.Create(productStock);

            return productStock;
        }


        public async Task<ProductStockReadDto> UpdateAsync(int id, ProductStockWriteDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid stock data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var psOnRepo = await GetByIdAsync(id);
            _mapper.Map(dto, psOnRepo);

            _stockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(psOnRepo);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> AddProductQuantityAsync(int id, int quantity)
        {
            if (quantity <= 0)
            {
                throw new AppException()
                    .SetTitle("Error adding quantity")
                    .SetDetail("Quantity should be a positive number")
                    .SetInstance(StockInstancePath(id));
            }

            var productStock = await GetByIdAsync(id);
            if (productStock.Quantity >= 0 && (productStock.Quantity + quantity < 0)) //overflow test
            {
                throw new AppException()
                    .SetTitle("Error adding quantity")
                    .SetDetail("Stock's [Quantity] value will overflow")
                    .SetInstance(StockInstancePath(id));
            }

            productStock.Quantity += quantity;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> RemoveProductQuantityAsync(int id, int quantity)
        {
            if (quantity <= 0)
            {
                throw new AppException()
                    .SetTitle("Error adding quantity")
                    .SetDetail("Quantity should be a positive number")
                    .SetInstance(StockInstancePath(id));
            }

            var productStock = await GetByIdAsync(id);
            if (productStock.Quantity - quantity < 0)
            {
                throw new AppException()
                    .SetTitle($"Not enough products available")
                    .SetDetail($"Only [{productStock.Quantity}] on stock")
                    .SetInstance(StockInstancePath(id));
            }

            productStock.Quantity -= quantity;
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