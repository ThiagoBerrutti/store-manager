namespace SalesAPI.Dtos
{
    public class ProductStockReadDto
    {
        //public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
    }
}