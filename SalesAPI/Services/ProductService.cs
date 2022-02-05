using AutoMapper;
using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Exceptions.Domain;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IStockService stockService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<ProductReadDto> CreateAsync(ProductWriteDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            _stockService.CreateProductStock(product, 0);
            await _unitOfWork.CompleteAsync();

            var stockDto = _mapper.Map<ProductReadDto>(product);

            return stockDto;
        }


        public async Task<IEnumerable<ProductReadDto>> GetAllDtoAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDto>>(products);

            return productsDto;
        }


        public async Task<ProductReadDto> GetDtoByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new DomainNotFoundException($"Product [Id = {id}] not found.");
            }
            var dto = _mapper.Map<ProductReadDto>(product);

            return dto;
        }


        public async Task<ProductReadDto> UpdateAsync(int id, ProductWriteDto productDto)
        {
            var productOnRepo = await _productRepository.GetByIdAsync(id);
            if (productOnRepo == null)
            {
                throw new DomainNotFoundException($"Product id [{id}] not found.");
            }

            _mapper.Map(productDto, productOnRepo);
            _productRepository.Update(productOnRepo);

            await _unitOfWork.CompleteAsync();

            var productReadDto = _mapper.Map<ProductReadDto>(productOnRepo);
            return productReadDto;
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new DomainNotFoundException($"Product id {id} not found");
            }

            _productRepository.Delete(product);

            await _unitOfWork.CompleteAsync();
        }
    }
}