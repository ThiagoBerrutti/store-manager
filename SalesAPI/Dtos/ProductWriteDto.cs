using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class ProductWriteDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} length must be between {2} and {1}")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public double Price { get; set; }
    }
}