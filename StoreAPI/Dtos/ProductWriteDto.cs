using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Dtos
{
    public class ProductWriteDto
    {        
        public string Name { get; set; }

        public string Description { get; set; }
                
        public double Price { get; set; }
    }
}