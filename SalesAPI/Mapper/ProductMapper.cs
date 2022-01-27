using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public Product MapDtoToEntity(ProductWriteDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public Product MapDtoToEntity(ProductWriteDto dto, Product mapInto)
        {
            mapInto.Name = dto.Name;
            mapInto.Price = dto.Price;

            return new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public IEnumerable<ProductReadDto> MapEntityToDtoList(IEnumerable<Product> productList)
        {
            var result = new List<ProductReadDto>();

            foreach(Product p in productList)
            {
                var dto = MapEntityToDto(p);
                result.Add(dto);
            }

            return result;
        }


        public ProductReadDto MapEntityToDto(Product product)
        {
            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}