using SalesAPI.Mapper;
using SalesAPI.Models;
using SalesAPI.Repositories;
using SalesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IProductMapper _productMapper;
        public IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IProductMapper productMapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productMapper = productMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProductAsync(ProductWriteDto productDto)
        {
            var product = _productMapper.DtoToEntity(productDto);

            _productRepository.Add(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            

            var a = await _productRepository.GetAll();
            return a;
        }

        public async Task<Product> GetAsync(Product product)
        {
            return await GetByIdAsync(product.Id);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ProductWriteDto productDto)
        {
            var product = _productMapper.DtoToEntity(productDto);
            _productRepository.Update(product);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _productRepository.Delete(product);

            await _unitOfWork.CompleteAsync();
        }
    }
}
