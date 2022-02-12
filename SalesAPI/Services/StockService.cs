using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SalesAPI.Dtos;
using SalesAPI.Exceptions;
using SalesAPI.Helpers;
using SalesAPI.Models;
using SalesAPI.Persistence.Repositories;
using SalesAPI.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class StockService : IStockService
    {
        private readonly string InstanceRouteName = nameof(Controllers.StockController.GetStockById);

        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LinkGenerator _linkGenerator;

    


        //private readonly IStockValidator _stockValidator;

        public StockService(IStockRepository productStockRepository, IMapper mapper, IUnitOfWork unitOfWork, LinkGenerator linkGenerator)
        //, IStockValidator stockValidator)
        {
            _stockRepository = productStockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _linkGenerator = linkGenerator;
        }


        public async Task<IEnumerable<ProductStockReadDto>> GetAllDtoAsync()
        {
            var stockList = await _stockRepository.GetAll();
            var dtoList = _mapper.Map<IEnumerable<ProductStock>, IEnumerable<ProductStockReadDto>>(stockList);

            return dtoList;
        }


        public async Task<ProductStock> GetByProductIdAsync(int productId)
        {
            var stock = await _stockRepository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                throw new DomainNotFoundException()
                    .SetTitle("Stock not found")
                    .SetDetail($"Stock for product [productId = {productId}] not found");
            }

            return stock;
        }


        public async Task<ProductStockReadDto> GetDtoByProductIdAsync(int productId)
        {
            var stock = await GetByProductIdAsync(productId);
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            return dto;
        }


        public async Task<ProductStock> GetByIdAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                throw new DomainNotFoundException()
                                    .SetTitle("Stock not found")
                                    .SetDetail($"Stock [Id = {id}] not found.");
            }

            return stock;
        }


        public async Task<ProductStockReadDto> GetDtoByIdAsync(int id)
        {
            var stock = await GetByIdAsync(id);
            var dto = _mapper.Map<ProductStockReadDto>(stock);

            return dto;
        }


        public ProductStock CreateProductStock(Product product, int startingAmount = 0)
        {
            var productStock = new ProductStock(product, startingAmount);
            _stockRepository.Create(productStock);

            return productStock;
        }


        public async Task<ProductStockReadDto> UpdateAsync(int id, ProductStockWriteDto dto)
        {
            //var validationResult = _stockValidator.Validate(dto);
            //if (!validationResult.IsValid)
            //{
            //    validationResult.Errors
            //}

            var psOnRepo = await GetByIdAsync(id);
            _mapper.Map(dto, psOnRepo);

            _stockRepository.Update(psOnRepo);

            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(psOnRepo);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> AddProductAmountAsync(int id, int amount)
        {
            if (amount <= 0)
            {                          
                var instance = _linkGenerator.GetPathByName(InstanceRouteName, new { id });                
                throw new ApplicationException()
                    .SetTitle("Error adding amount")
                    .SetDetail("Amount should be a positive number")
                    .SetInstance($"{instance}");
            }

            var productStock = await GetByIdAsync(id);
            if (productStock.Count >= 0 && (productStock.Count + amount < 0)) //overflow test
            {
                var instance = _linkGenerator.GetPathByName(InstanceRouteName, new { id });
                throw new ApplicationException()
                    .SetTitle("Error adding amount")
                    .SetDetail("Stock's [Count] value will overflow")
                    .SetInstance($"{instance}");
            }

            productStock.Count += amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }


        public async Task<ProductStockReadDto> RemoveProductAmountAsync(int id, int amount)
        {
            if (amount <= 0)
            {
                var instance = _linkGenerator.GetPathByName(InstanceRouteName, new { id });
                throw new ApplicationException()
                    .SetTitle("Error adding amount")
                    .SetDetail("Amount should be a positive number")
                    .SetInstance($"{instance}");
            }

            var productStock = await GetByIdAsync(id);
            if (productStock.Count - amount < 0)
            {
                var instance = _linkGenerator.GetPathByName(InstanceRouteName, new { id });
                throw new ApplicationException()
                    .SetTitle($"Not enough products available")
                    .SetDetail($"Only [{productStock.Count}] on stock")
                    .SetInstance($"{instance}");

            }

            productStock.Count -= amount;
            await _unitOfWork.CompleteAsync();

            var productStockDto = _mapper.Map<ProductStockReadDto>(productStock);
            return productStockDto;
        }
    }
}