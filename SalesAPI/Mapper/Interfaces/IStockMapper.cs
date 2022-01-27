using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public interface IStockMapper
    {
        public ProductStock MapDtoToEntity(StockWriteDto dto);
        public ProductStock MapDtoToEntity(StockWriteDto dto, ProductStock mapInto);

        public StockReadDto MapEntityToDto(ProductStock product);
        public IEnumerable<StockReadDto> MapEntityToDtoList(IEnumerable<ProductStock> entityList);

    }
}