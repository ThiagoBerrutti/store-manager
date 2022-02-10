using System;
using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} must have between {2} and {1} characters minimum.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "{0} must have between {2} and {1} characters minimum.")]
        public string Password { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} length can't be more than {1}.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} length can't be more than {1}.")]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}