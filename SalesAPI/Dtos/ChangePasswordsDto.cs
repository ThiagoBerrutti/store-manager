using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class ChangePasswordsDto
    {
        [Required]
        [MinLength(6, ErrorMessage = "{0} length can't be less than {1}.")]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "{0} length can't be less than {1}.")]
        public string NewPassword { get; set; }
    }
}