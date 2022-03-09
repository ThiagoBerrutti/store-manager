using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StoreAPI.Domain;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Infra;
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


        public async Task<ServiceResponse<PaginatedList<ProductStockReadDto>>> GetAllDtoPaginatedAsync(StockParametersDto parameters)
        {
            var validationResult = _stockParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<PaginatedList<ProductStockReadDto>>(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid query string parameters. Check '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            Expression<Func<ProductStock, bool>> expression =
                s =>
                    (string.IsNullOrEmpty(parameters.ProductName) || s.Product.Name.ToLower().Contains(parameters.ProductName.ToLower())) &&
                    (!parameters.QuantityMin.HasValue || s.Quantity >= parameters.QuantityMin) &&
                    (!parameters.QuantityMax.HasValue || s.Quantity <= parameters.QuantityMax);


            var page = await _stockRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);

            var dto = _mapper.Map<PaginatedList<ProductStock>, PaginatedList<ProductStockReadDto>>(page);
            var result = new ServiceResponse<PaginatedList<ProductStockReadDto>>(dto);

            return result;
        }

        // return the entity without any validation or mapping
        //public async Task<ProductStock> GetByProductIdAsync(int productId)
        //{
        //    var stock = await _stockRepository.GetByProductIdAsync(productId);
        //    if (stock == null)
        //    {
        //        throw new DomainNotFoundException()
        //            .SetTitle("Stock not found")
        //            .SetDetail($"Stock for product [productId = {productId}] not found");
        //    }

        //    return stock;
        //}


        public async Task<ServiceResponse<ProductStockReadDto>> GetDtoByProductIdAsync(int productId)
        {
            var validationResult = new ValidationResult().ValidateId(productId, "Product Id");

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductStockReadDto>(validationResult)
                    .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var stock = await _stockRepository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                return new ServiceResponse<ProductStockReadDto>()
                    .SetTitle("Stock not found.")
                    .SetDetail($"Stock for product [Id = {productId}]' not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStock>> GetByIdAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return new ServiceResponse<ProductStock>()
                            .SetTitle("Stock not found")
                            .SetDetail($"Stock [Id = {id}] not found.")
                            .SetStatus(StatusCodes.Status404NotFound);
            }

            return new ServiceResponse<ProductStock>(stock);
        }


        public async Task<ServiceResponse<ProductStockReadDto>> GetDtoByIdAsync(int id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Product stock Id");

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductStockReadDto>(validationResult)
                    .SetDetail($"Invalid stock data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new ServiceResponse<ProductStockReadDto>(stockResponse.Error);
            }
            var stock = stockResponse.Data;
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        // called by ProductService.Create only
        public ProductStock CreateProductStock(Product product, int startingQuantity = AppConstants.Validations.Stock.QuantityMinValue)
        {
            var productStock = new ProductStock(product, startingQuantity);
            _stockRepository.Create(productStock);

            return productStock;
        }

        //test
        public async Task<ServiceResponse<ProductStockReadDto>> UpdateAsync(int id, ProductStockWriteDto stockUpdate)
        {
            var validationResult = _validator.Validate(stockUpdate)
                                             .ValidateId(id, "Product stock Id");

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductStockReadDto>(validationResult)
                            .SetTitle("Validation error")
                            .SetDetail($"Invalid stock data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new ServiceResponse<ProductStockReadDto>(stockResponse.Error);
            }

            var stock = stockResponse.Data;
            _mapper.Map(stockUpdate, stock);

            _stockRepository.Update(stock);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductStockReadDto>(stock);
            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStockReadDto>> AddProductQuantityAsync(int id, int quantity)
        {
            var validationResult = new ValidationResult()
                                            .ValidateId(id, "Product stock Id")
                                            .GreaterThan(quantity, 0, "Quantity");

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductStockReadDto>(validationResult)
                            .SetTitle("Validation error")
                            .SetDetail($"Error adding quantity to stock. See '{ServiceResponse<ProductStockReadDto>.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new ServiceResponse<ProductStockReadDto>(stockResponse.Error);
            }

            var stock = stockResponse.Data;

            if (stock.Quantity >= 0 && (stock.Quantity + quantity < 0)) //overflow test
            {
                return new ServiceResponse<ProductStockReadDto>()
                    .SetTitle("Operation error")
                    .SetDetail("Error adding to stock. Product stock quantity value will overflow. Contact the support")
                    .SetInstance(StockInstancePath(id));
            }

            stock.Quantity += quantity;
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductStockReadDto>(stock);
            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductStockReadDto>> RemoveProductQuantityAsync(int id, int quantity)
        {
            var validationResult = new ValidationResult()
                                            .ValidateId(id, "Product stock Id")
                                            .GreaterThan(quantity, 0, "Quantity");

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductStockReadDto>(validationResult)
                            .SetTitle("Validation error")
                            .SetDetail($"Invalid stock data. See '{ServiceResponse<ProductStockReadDto>.ErrorKey}' for more details");
            }

            var stockResponse = await GetByIdAsync(id);
            if (!stockResponse.Success)
            {
                return new ServiceResponse<ProductStockReadDto>(stockResponse.Error);
            }

            var stock = stockResponse.Data;

            if (stock.Quantity - quantity < AppConstants.Validations.Stock.QuantityMinValue)
            {
                return new ServiceResponse<ProductStockReadDto>()
                    .SetTitle($"Operation error")
                    .SetDetail($"Error removing product quantity from stock. Only [{stock.Quantity}] left on stock. Please insert a value less than or equal to {AppConstants.Validations.Stock.QuantityMinValue + stock.Quantity}")
                    .SetInstance(StockInstancePath(id));
            }

            stock.Quantity -= quantity;
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductStockReadDto>(stock);
            var result = new ServiceResponse<ProductStockReadDto>(dto);

            return result;
        }


        private string StockInstancePath(object id)
        {
            return _linkGenerator.GetPathByName(nameof(Controllers.StockController.GetStockById), new { id });
        }
    }
}