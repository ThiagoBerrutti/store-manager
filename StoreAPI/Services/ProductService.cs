using AutoMapper;
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



        public async Task<PaginatedList<ProductReadDto>> GetAllDtoPaginatedAsync(ProductParametersDto parameters)
        {
            var validationResult = _productParametersValidator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid query string parameters. Check '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            Expression<Func<Product, bool>> expression =
                p =>
                    (!parameters.MinPrice.HasValue || p.Price >= parameters.MinPrice) &&
                    (!parameters.MaxPrice.HasValue || p.Price <= parameters.MaxPrice) &&
                    (string.IsNullOrEmpty(parameters.Name) || p.Name.ToLower().Contains(parameters.Name.ToLower())) &&
                    (string.IsNullOrEmpty(parameters.Name) || p.Description.ToLower().Contains(parameters.Description.ToLower())) &&
                    (!parameters.OnStock.HasValue || ((p.ProductStock.Quantity > 0) == parameters.OnStock.Value));

            var result = await _productRepository.GetAllWherePaginatedAsync(parameters.PageNumber, parameters.PageSize, expression);

            var dto = _mapper.Map<PaginatedList<Product>, PaginatedList<ProductReadDto>>(result);

            return dto;
        }


        public async Task<ProductReadWithStockDto> CreateAsync(ProductWriteDto productDto, int quantity)
        {
            AppCustomValidator.GreaterThanOrEqualTo(quantity, AppConstants.Validations.Stock.QuantityMinValue, "Quantity");

            var validationResult = _productValidator.Validate(productDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }

            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            _stockService.CreateProductStock(product, quantity);
            await _unitOfWork.CompleteAsync();

            var productWithStockDto = _mapper.Map<ProductReadWithStockDto>(product);

            return productWithStockDto;
        }


        public async Task<ProductReadDto> GetDtoByIdAsync(int id)
        {
            AppCustomValidator.ValidateId(id, "Product Id");

            var product = await GetByIdAsync(id);
            var dto = _mapper.Map<ProductReadDto>(product);

            return dto;
        }


        public async Task<Product> GetByIdAsync(int id)
        {
            AppCustomValidator.ValidateId(id, "Product Id");

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new DomainNotFoundException()
                    .SetTitle("Product not found")
                    .SetDetail($"Product [Id = {id}] not found.");
            }

            return product;
        }


        public async Task<ProductReadDto> UpdateAsync(int id, ProductWriteDto productDto)
        {
            AppCustomValidator.ValidateId(id, "Product Id");

            var validationResult = _productValidator.Validate(productDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult)
                    .SetTitle("Validation error")
                    .SetDetail($"Invalid product data. See '{ExceptionWithProblemDetails.ErrorKey}' for more details");
            }
            var productOnRepo = await GetByIdAsync(id);

            _mapper.Map(productDto, productOnRepo);
            _productRepository.Update(productOnRepo);

            await _unitOfWork.CompleteAsync();

            var productReadDto = _mapper.Map<ProductReadDto>(productOnRepo);
            return productReadDto;
        }


        public async Task DeleteAsync(int id)
        {
            AppCustomValidator.ValidateId(id, "Product Id");

            var product = await GetByIdAsync(id);
            _productRepository.Delete(product);

            await _unitOfWork.CompleteAsync();
        }
    }
}