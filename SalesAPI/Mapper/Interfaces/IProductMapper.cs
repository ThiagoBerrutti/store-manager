using SalesAPI.Models;
using SalesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Mapper
{
    public interface IProductMapper
    {
        public Product DtoToEntity(ProductWriteDto dto);

        public ProductReadDto MapEntityToDto(Product product);
    }        
}
