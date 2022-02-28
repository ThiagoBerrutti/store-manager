namespace StoreAPI.Domain
{
    public class ProductStock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public ProductStock()
        {
        }

        public ProductStock(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}