using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Mapper;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class StockService : IStockService
    {
        private StockRepository _productStockRepository;
        private IProductRepository _productRepository;
        private IStockMapper _stockMapper;
        public IUnitOfWork _unitOfWork;

        public StockService(StockRepository productStockRepository,IProductRepository productRepository,
                            IStockMapper stockMapper, IUnitOfWork unitOfWork)
        {
            _productStockRepository = productStockRepository;
            _productRepository = productRepository;
            _stockMapper = stockMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProductStock(int productId, int startingAmount = 0)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new ServiceException($"No product with id [{productId}]");
            }

            var productStock = new ProductStock(productId, startingAmount);
            _productStockRepository.Create(productStock);

            await _unitOfWork.CompleteAsync();
        }

        public void CreateProductStock(Product product, int startingAmount = 0)
        {
            var productStock = new ProductStock(product, startingAmount);
            _productStockRepository.Create(productStock);
        }

        public async Task AddProductAmount(int productId, int amount)
        {
            var productStock = await _productStockRepository.GetByProductIdAsync(productId);
            if (productStock == null)
            {
                throw new StockException($"No stock found with [productId{productId}]");
            }

            productStock.Count += amount;
            await _unitOfWork.CompleteAsync();
        }

        public async Task Update(int productId, StockWriteDto dto)
        { 
            var psOnRepo = await _productStockRepository.GetByProductIdAsync(productId);
            _stockMapper.MapDtoToEntity(dto, psOnRepo);

            _productStockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveProductAmount(int productId, int amount)
        {
            var productStock = await _productStockRepository.GetByProductIdAsync(productId);
            if (productStock == null)
            {
                throw new StockException($"No stock found with [productId = {productId}]");
            }

            if (productStock.Count - amount < 0)
            {
                throw new StockException($"Not enough products available. Only [{productStock.Count}] on stock.");
            }

            productStock.Count -= amount;
            await _unitOfWork.CompleteAsync();
        }

        public async Task<StockReadDto> GetByProductId(int productId)
        {
            var productStock = await _productStockRepository.GetByProductIdAsync(productId);
            if (productStock == null)
            {
                throw new StockException($"No stock found for [productId = {productId}]");
            }

            var dto = _stockMapper.MapEntityToDto(productStock);

            return dto;
        }

        public async Task<IEnumerable<StockReadDto>> GetAllAsync()
        {
            var stockList = await _productStockRepository.GetAll();
            var dtoList = _stockMapper.MapEntityToDtoList(stockList);

            return dtoList;
        }

        public async Task Delete(int id) 
        {
            var stockOnRepo = await _productStockRepository.GetByIdAsync(id);
            if (stockOnRepo == null)
            {
                throw new StockException($"No stock [Id = {id}] found.");
            }

            _productStockRepository.Delete(stockOnRepo);

            await _unitOfWork.CompleteAsync();
        }

        public async Task Clear()
        {
            var stocks = await _productStockRepository.GetAll();

            foreach (ProductStock ps in stocks)
            {
                _productStockRepository.Delete(ps);
            }

            await _unitOfWork.CompleteAsync();
        }

    }
}