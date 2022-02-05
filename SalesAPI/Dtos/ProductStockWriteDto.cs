using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class ProductStockWriteDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Count must be a positive number or zero")]
        public int Count { get; set; }
    }
}