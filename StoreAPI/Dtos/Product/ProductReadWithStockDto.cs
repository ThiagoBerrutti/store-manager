namespace StoreAPI.Dtos
{
    /// <summary>
    /// Data about a registered product and it's respective product stock data
    /// </summary>
    public class ProductReadWithStockDto
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>123</example>
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Banana</example>
        public string Name { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Banana caturra 1kg</example>
        public string Description { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        /// <example>5.99</example>
        public double Price { get; set; }

        /// <summary>
        /// Product stock object
        /// </summary>
        public ProductStockReadDto ProductStock { get; set; }
    }
}