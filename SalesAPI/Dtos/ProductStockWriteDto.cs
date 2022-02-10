using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class ProductStockWriteDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a greater or equal to zero")]
        public int Count { get; set; }
    }
}