using System;
using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class UserParametersDto : QueryStringParameterDto
    {
        public DateTime MinDateOfBirth { get; set; } = new DateTime();
        public DateTime MaxDateOfBirth { get; set; } = DateTime.Today;
        //public int? RoleId { get; set; }
        public List<int> RoleId { get; set; } = new List<int>();
        public string UserName { get; set; } = "";
        public bool IsExactUserName { get; set; } = false;
        public string Name = "";
        public bool IsExactName { get; set; } = false;
    }
}