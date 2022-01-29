using SalesAPI.Exceptions;
using SalesAPI.Mapper;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using SalesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesAPI.Persistence;

namespace SalesAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IProductMapper _productMapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IStockService stockService,
            IProductMapper productMapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _productMapper = productMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(ProductWriteDto productDto)
        {
            var product = _productMapper.MapDtoToEntity(productDto);
            _productRepository.Add(product);

            _stockService.CreateProductStock(product, 0);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsDto = _productMapper.MapEntityToDtoList(products);

            return productsDto;
        }

        public async Task<ProductReadDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var dto = _productMapper.MapEntityToDto(product);

            return dto;
        }

        public async Task UpdateAsync(int id, ProductWriteDto productDto)
        {
            var productOnRepo = await _productRepository.GetByIdAsync(id);
            if (productOnRepo == null)
            {
                throw new EntityNotFoundException($"Product id [{id}] not found.");
            }

            _productMapper.MapDtoToEntity(productDto, productOnRepo);
            _productRepository.Update(productOnRepo);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new EntityNotFoundException($"Product id {id} not found");
            }

            _productRepository.Delete(product);

            await _unitOfWork.CompleteAsync();
        }

        public async Task Clear()
        {
            var products = await _productRepository.GetAllAsync();

            foreach (Product p in products)
            {
                _productRepository.Delete(p);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}