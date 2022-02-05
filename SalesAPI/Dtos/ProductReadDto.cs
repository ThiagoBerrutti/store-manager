namespace SalesAPI.Dtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductStockId { get; set; }
    }
}