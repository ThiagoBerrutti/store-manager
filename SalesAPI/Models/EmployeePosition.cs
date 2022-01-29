using System.Collections.Generic;

namespace SalesAPI.Models
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}