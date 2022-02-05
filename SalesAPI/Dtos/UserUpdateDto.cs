using System;
using System.ComponentModel.DataAnnotations;

namespace SalesAPI.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        [StringLength(150, ErrorMessage = "{0} length can't be more than {1}.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} length can't be more than {1}.")]
        public string LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}