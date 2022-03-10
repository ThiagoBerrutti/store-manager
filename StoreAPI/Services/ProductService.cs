using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using StoreAPI.Domain;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Infra;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Services.Communication;
using StoreAPI.Validations;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductValidator _productValidator;
        private readonly ProductParametersValidator _productParametersValidator;

        public ProductService(IProductRepository productRepository, IStockService stockService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productValidator = new ProductValidator();
            _productParametersValidator = new ProductParametersValidator();
        }



        public async Task<ServiceResponse<PaginatedList<ProductReadDto>>> GetAllDtoPaginatedAsync(ProductParametersDto parameters)
        {
            var validationResult = _productParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<PaginatedList<ProductReadDto>>()
                            .HasFailed(validationResult)
                            .SetDetail($"Invalid query string parameters. Check '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            Expression<Func<Product, bool>> expression =
                p =>
                    (!parameters.MinPrice.HasValue || p.Price >= parameters.MinPrice) &&
                    (!parameters.MaxPrice.HasValue || p.Price <= parameters.MaxPrice) &&
                    (string.IsNullOrEmpty(parameters.Name) || p.Name.ToLower().Contains(parameters.Name.ToLower())) &&
                    (string.IsNullOrEmpty(parameters.Description) || p.Description.ToLower().Contains(parameters.Description.ToLower())) &&
                    (!parameters.OnStock.HasValue || ((p.ProductStock.Quantity > 0) == parameters.OnStock.Value));

            var page = await _productRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);
            var dto = _mapper.Map<PaginatedList<Product>, PaginatedList<ProductReadDto>>(page);

            var result = new ServiceResponse<PaginatedList<ProductReadDto>>(dto);

            return result;
        }


        public async Task<ServiceResponse<ProductReadWithStockDto>> CreateAsync(ProductWriteDto productDto, int quantity)
        {
            var validationResult = new ValidationResult()
                        .GreaterThanOrEqualTo(quantity, AppConstants.Validations.Stock.QuantityMinValue, "Quantity")
                        .AddFailuresFrom(_productValidator.Validate(productDto));

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductReadWithStockDto>()
                            .HasFailed(validationResult)
                            .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            var response = _stockService.CreateProductStock(product, quantity);
            
            await _unitOfWork.CompleteAsync();

            var productWithStockDto = _mapper.Map<ProductReadWithStockDto>(product);
            var result = new ServiceResponse<ProductReadWithStockDto>(productWithStockDto);

            return result;
        }


        public async Task<ServiceResponse<ProductReadDto>> GetDtoByIdAsync(int id)
        {
            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new ServiceResponse<ProductReadDto>().HasFailed(response.Error);
            }

            var dto = _mapper.Map<ProductReadDto>(response.Data);
            var result = new ServiceResponse<ProductReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse<Product>> GetByIdAsync(int id)
        {
            var validationResponse = new ValidationResult().ValidateId(id, "Product Id");
            if (!validationResponse.IsValid)
            {
                return new ServiceResponse<Product>()
                            .HasFailed(validationResponse)
                            .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new ServiceResponse<Product>()
                            .HasFailed()
                            .SetTitle("Product not found")
                            .SetDetail($"Product [Id = {id}] not found.")
                            .SetStatus(StatusCodes.Status404NotFound);
            }

            var result = new ServiceResponse<Product>(product);

            return new ServiceResponse<Product>(product);
        }


        public async Task<ServiceResponse<ProductReadDto>> UpdateAsync(int id, ProductWriteDto productDto)
        {
            var validationResult = new ValidationResult()
                        .ValidateId(id, "Product Id")
                        .AddFailuresFrom(_productValidator.Validate(productDto));

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductReadDto>()
                            .HasFailed(validationResult)
                            .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var productResponse = await GetByIdAsync(id);
            if (!productResponse.Success)
            {
                return new ServiceResponse<ProductReadDto>().HasFailed(productResponse.Error);
            }

            var product = productResponse.Data;

            _mapper.Map(productDto, product);
            _productRepository.Update(product);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ProductReadDto>(product);
            var result = new ServiceResponse<ProductReadDto>(dto);

            return result;
        }


        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var validationResult = new ValidationResult().ValidateId(id, "Product Id");
            if (!validationResult.IsValid)
            {
                return new ServiceResponse()
                            .HasFailed(validationResult)
                            .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new ServiceResponse().HasFailed(response.Error);
            }

            var product = response.Data;

            _productRepository.Delete(product);
            await _unitOfWork.CompleteAsync();

            return new ServiceResponse();
        }
    }
}