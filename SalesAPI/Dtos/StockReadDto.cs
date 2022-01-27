namespace SalesAPI.Dtos
{
    public class StockReadDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}