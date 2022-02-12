using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class ProductWriteDto
    {        
        public string Name { get; set; }
        
        public string Description { get; set; }
                
        public double Price { get; set; }
    }
}