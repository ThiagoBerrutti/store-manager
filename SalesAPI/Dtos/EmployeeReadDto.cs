namespace SalesAPI.Dtos
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Role { get; set; }
        //public int? RoleId { get; set; }
        public decimal BaseSalary { get; set; }
    }
}