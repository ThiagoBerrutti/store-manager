using System;

namespace SalesAPI.Dtos
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}