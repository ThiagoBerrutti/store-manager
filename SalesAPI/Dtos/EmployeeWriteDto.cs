using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Dtos
{
    public class EmployeeWriteDto
    {
        public string Name { get; set; }
        //public int RoleId { get; set; }

        public decimal BaseSalary { get; set; }
    }
}
