using SalesAPI.Models;
using SalesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public Product DtoToEntity(ProductWriteDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public ProductReadDto MapEntityToDto(Product product)
        {
            return new ProductReadDto
            {
                Name = product.Name,
                Price = product.Price
            };
        }



    }
}
