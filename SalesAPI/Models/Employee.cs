namespace SalesAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployeePosition Position { get; set; }
        public int? PositionId { get; set; }
        public decimal BaseSalary { get; set; }
    }
}