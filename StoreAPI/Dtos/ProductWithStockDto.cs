namespace StoreAPI.Dtos
{
    public class ProductWithStockDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public ProductStockReadDto ProductStock { get; set; }
    }
}