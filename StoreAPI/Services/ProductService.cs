using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using StoreAPI.Dtos;
using StoreAPI.Exceptions;
using StoreAPI.Extensions;
using StoreAPI.Models;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductValidator _validator;

        public ProductService(IProductRepository productRepository, IStockService stockService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = new ProductValidator();
        }


        public async Task<ProductWithStockDto> CreateAsync(ProductWriteDto productDto, int amount)
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

            var productWithStockDto = _mapper.Map<ProductWithStockDto>(product);

            return productWithStockDto;
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
            var products = await _productRepository.GetAllAsync();
            var nameRes = products
                .Where(p => p.Name.ToLower().Contains(search))
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id);

            var descriptionRes = products
                .Where(p => p.Description.ToLower().Contains(search) && !nameRes.Contains(p))
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id);

            var results = nameRes.Concat(descriptionRes);
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