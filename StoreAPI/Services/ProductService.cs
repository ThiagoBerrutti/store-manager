using AutoMapper;
using Microsoft.AspNetCore.Http;
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
                return new ServiceResponse<PaginatedList<ProductReadDto>>(validationResult)
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
            var validationResult = AppCustomValidator.GreaterThanOrEqualTo(quantity, AppConstants.Validations.Stock.QuantityMinValue, "Quantity")
                .AddValidationFailuresFrom(_productValidator.Validate(productDto));
            
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductReadWithStockDto>(validationResult)
                    .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            _stockService.CreateProductStock(product, quantity);
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
                return new ServiceResponse<ProductReadDto>(response.Error);
            }

            var dto = _mapper.Map<ProductReadDto>(response.Data);

            return new ServiceResponse<ProductReadDto>(dto);
        }


        public async Task<ServiceResponse<Product>> GetByIdAsync(int id)
        {
            var validationResponse = AppCustomValidator.ValidateId(id, "Product Id");
            if (!validationResponse.IsValid)
            {
                return new ServiceResponse<Product>(validationResponse)
                    .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new ServiceResponse<Product>()
                    .SetTitle("Product not found")
                    .SetDetail($"Product [Id = {id}] not found.")
                    .SetStatus(StatusCodes.Status404NotFound);
            }

            return new ServiceResponse<Product>(product);
        }


        public async Task<ServiceResponse<ProductReadDto>> UpdateAsync(int id, ProductWriteDto productDto)
        {
            var validationResult = AppCustomValidator.ValidateId(id, "Product Id");
            validationResult.AddValidationFailuresFrom(_productValidator.Validate(productDto));

            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductReadDto>(validationResult)
                            .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }
            var productResponse = await GetByIdAsync(id);
            if (!productResponse.Success)
            {
                return new ServiceResponse<ProductReadDto>(productResponse.Error);
            }

            var product = productResponse.Data;

            _mapper.Map(productDto, product);
            _productRepository.Update(product);
            await _unitOfWork.CompleteAsync();

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            return new ServiceResponse<ProductReadDto>(productReadDto);
        }


        public async Task<ServiceResponse<ProductReadDto>> DeleteAsync(int id)
        {
            var validationResult = AppCustomValidator.ValidateId(id, "Product Id");
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<ProductReadDto>(validationResult)
                            .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var response = await GetByIdAsync(id);
            if (!response.Success)
            {
                return new ServiceResponse<ProductReadDto>(response.Error);
            }

            var product = response.Data;

            _productRepository.Delete(product);

            await _unitOfWork.CompleteAsync();

            return new ServiceResponse<ProductReadDto>();
        }
    }
}