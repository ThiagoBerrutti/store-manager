using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class ProductWriteDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} length must be between {2} and {1}")]
        public string Name { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} maximum length is {1}")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be greater or equal to zero")]
        public double Price { get; set; }
    }
}