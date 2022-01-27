using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public class StockMapper : IStockMapper
    {
        public ProductStock MapDtoToEntity(StockWriteDto dto)
        {
            return new ProductStock(
                dto.ProductId,
                dto.Count);
        }

        public ProductStock MapDtoToEntity(StockWriteDto dto, ProductStock mapInto)
        {
            mapInto.Count = dto.Count;
            mapInto.ProductId = dto.ProductId;

            return new ProductStock(
                dto.ProductId,
                dto.Count);
        }

        public StockReadDto MapEntityToDto(ProductStock entity)
        {
            return new StockReadDto
            {
                Id = entity.Id,
                Count = entity.Count,
                ProductId = entity.ProductId,
                ProductName = entity.Product.Name
            };
        }

        public IEnumerable<StockReadDto> MapEntityToDtoList(IEnumerable<ProductStock> entityList)
        {
            var result = new List<StockReadDto>();

            foreach(ProductStock ps in entityList)
            {
                var dto = MapEntityToDto(ps);
                result.Add(dto);
            }

            return result;
        }

    }
}