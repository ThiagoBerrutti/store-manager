using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Extensions;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using SalesAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductValidator _validator;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository, IStockService stockService, IMapper mapper, IUnitOfWork unitOfWork)
            //, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = new ProductValidator();
            //_httpContextAccessor = httpContextAccessor;
        }


        public async Task<ProductReadDto> CreateAsync(ProductWriteDto productDto, int amount)
        {
            if (amount < 0 || amount > int.MaxValue)
            {
                throw new AppException()
                    .SetTitle("Error creating product")
                    .SetDetail("Amount should be greater or equal to zero");                    
            }            
            
            var validationResult = _validator.Validate(productDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid product data. See 'errors' for more details")
                    .SetErrors(validationResult.Errors.Select(e => e.ErrorMessage)); ;
            }

            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            _stockService.CreateProductStock(product, amount);
            await _unitOfWork.CompleteAsync();

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            return productReadDto;
        }


        public async Task<IEnumerable<ProductReadDto>> GetAllDtoAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDto>>(products);

            return productsDto;
        }


        public async Task<ProductReadDto> GetDtoByIdAsync(int id)
        {
            var product = await GetByIdAsync(id);
            var dto = _mapper.Map<ProductReadDto>(product);

            return dto;
        }


        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new DomainNotFoundException()
                    .SetTitle("Product not found")
                    .SetDetail($"Product [Id = {id}] not found.");
            }

            return product;
        }


        public async Task<IEnumerable<ProductReadDto>> SearchDtosAsync(string search)
        {
            var results = await _productRepository.SearchAsync(search);

            var productsDto = _mapper.Map<IEnumerable<ProductReadDto>>(results);
            return productsDto;
        }



        public async Task<ProductReadDto> UpdateAsync(int id, ProductWriteDto productDto)
        {
            var result = _validator.Validate(productDto);
            if (!result.IsValid)
            {
                throw new AppValidationException()
                    .SetTitle("Validation error")
                    .SetDetail("Invalid product data. See 'errors' for more details")
                    .SetErrors(result.Errors.Select(e => e.ErrorMessage));
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
            var product = await GetByIdAsync(id);
            _productRepository.Delete(product);

            await _unitOfWork.CompleteAsync();
        }
    }
}