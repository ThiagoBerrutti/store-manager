using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class RoleWriteDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "{0} can't be empty.")]
        public string Name { get; set; }
    }
}