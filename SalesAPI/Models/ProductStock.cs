namespace SalesAPI.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public ProductStock()
        {
        }

        public ProductStock(Product product, int count)
        {
            Product = product;
            Count = count;
        }
    }
}