using SalesAPI.Exceptions;
using SalesAPI.Mapper;
using SalesAPI.Models;
using SalesAPI.Repositories;
using SalesAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class StockService : IStockService
    {
        private StockRepository _productStockRepository;
        private IProductRepository _productRepository;
        private IProductMapper _productMapper;
        public IUnitOfWork _unitOfWork;

        public StockService(StockRepository productStockRepository, IProductRepository productRepository, IProductMapper productMapper, IUnitOfWork unitOfWork)
        {
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
            _productMapper = productMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddProduct(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new ServiceException($"No product with id [{productId}]");
            }

            var productStock = await _productStockRepository.GetByProductAsync(product);
            if (productStock == null)
            {
                productStock = new ProductStock(product, 0);
                _productStockRepository.Create(productStock);
            }

            productStock.Count += quantity;
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveProduct(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new ServiceException($"No product found with id [{productId}]");
            }

            var productStock = await _productStockRepository.GetByProductAsync(product);
            if (productStock == null)
            {
                throw new StockException($"Product {product.Name} stock not found");
            }

            if (productStock.Count-quantity < 0)
            {
                throw new StockException("Not enough products available on stock. Try removing less products");
            }

            productStock.Count -= quantity;
            await _unitOfWork.CompleteAsync();
        }

       
        public async Task<IAsyncEnumerable<ProductStock>> GetAllProducts()
        {
            return _productStockRepository.GetAll();
        }
    }
}