namespace SalesAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public Role Role { get; set; }
        //public int? RoleId { get; set; }
        public decimal BaseSalary { get; set; }
    }
}