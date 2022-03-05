namespace StoreAPI.Dtos
{
    /// <summary>
    /// Product stock information
    /// </summary>
    public class ProductStockReadDto
    {
        /// <summary>
        /// Product stock Id
        /// </summary>
        /// <example>123</example>
        public int Id { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>321</example>
        public int ProductId { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Banana</example>
        public string ProductName { get; set; }

        /// <summary>
        /// Product quantity on stock
        /// </summary>
        /// <example>9000</example>
        public int Quantity { get; set; }
    }
}