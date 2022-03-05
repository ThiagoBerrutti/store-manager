namespace StoreAPI.Dtos
{
    /// <summary>
    /// Product stock data to persist
    /// </summary>
    public class ProductStockWriteDto
    {
        /// <summary>
        /// Product quantity in stock
        /// </summary>
        /// <example>10</example>
        public int Quantity { get; set; }
    }
}