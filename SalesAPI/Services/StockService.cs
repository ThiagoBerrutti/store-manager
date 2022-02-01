﻿using AutoMapper;
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
        private readonly ProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(ProductStockRepository productStockRepository, IMapper mapper,
                            IUnitOfWork unitOfWork)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //public async Task CreateProductStock(int productId, int startingAmount = 0)
        //{
        //    var product = await _productRepository.GetByIdAsync(productId);
        //    if (product == null)
        //    {
        //        throw new EntityNotFoundException($"No product with id [{productId}]");
        //    }

        //    var productStock = new ProductStock(productId, startingAmount);
        //    _productStockRepository.Create(productStock);

        //    await _unitOfWork.CompleteAsync();
        //}

        public ProductStock CreateProductStock(Product product, int startingAmount = 0)
        {
            var productStock = new ProductStock(product, startingAmount);
            _productStockRepository.Create(productStock);

            return productStock;
        }

        public async Task AddProductAmount(int id, int amount)
        {
            if (amount <= 0)
            {
                throw new ApplicationException("Amount should be a positive number.");
            }

            var productStock = await _productStockRepository.GetByIdAsync(id);
            if (productStock == null)
            {
                throw new EntityNotFoundException($"No stock found with [Id{id}]");
            }

            productStock.Count += amount;
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveProductAmount(int id, int amount)
        {
            if (amount <= 0)
            {
                throw new ApplicationException("Amount should be a positive number.");
            }

            var productStock = await _productStockRepository.GetByIdAsync(id);
            if (productStock == null)
            {
                throw new EntityNotFoundException($"No stock found with [Id = {id}]");
            }

            if (productStock.Count - amount < 0)
            {
                throw new StockException($"Not enough products available. Only [{productStock.Count}] on stock.");
            }

            productStock.Count -= amount;
            await _unitOfWork.CompleteAsync();
        }
        

        public async Task Update(int productId, StockWriteDto dto)
        { 
            var psOnRepo = await _productStockRepository.GetByProductIdAsync(productId);
            _mapper.Map(dto, psOnRepo);

            _productStockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();
        }


        public async Task<StockReadDto> GetByProductId(int productId)
        {
            var productStock = await _productStockRepository.GetByProductIdAsync(productId);
            if (productStock == null)
            {
                throw new EntityNotFoundException($"No stock found for [productId = {productId}]");
            }

            var dto = _mapper.Map<StockReadDto>(productStock);

            return dto;
        }

        public async Task<IEnumerable<StockReadDto>> GetAllAsync()
        {
            var stockList = await _productStockRepository.GetAll();
            var dtoList = _mapper.Map<IEnumerable<ProductStock>, IEnumerable<StockReadDto>>(stockList);

            return dtoList;
        }

        public async Task Delete(int id) 
        {
            var stockOnRepo = await _productStockRepository.GetByIdAsync(id);
            if (stockOnRepo == null)
            {
                throw new EntityNotFoundException($"No stock [Id = {id}] found.");
            }

            stockOnRepo.Count = 0;

            await _unitOfWork.CompleteAsync();
        }
    }
}