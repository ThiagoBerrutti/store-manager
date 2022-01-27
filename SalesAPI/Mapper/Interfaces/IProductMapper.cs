using SalesAPI.Models;
using SalesAPI.Dtos;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public interface IProductMapper
    {
        public Product MapDtoToEntity(ProductWriteDto dto);
        public Product MapDtoToEntity(ProductWriteDto dto, Product mapInto);

        public ProductReadDto MapEntityToDto(Product product);
        public IEnumerable<ProductReadDto> MapEntityToDtoList(IEnumerable<Product> productList);

    }
}