namespace SalesAPI.Dtos
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? PositionId { get; set; }
        public decimal BaseSalary { get; set; }
    }
}