namespace StoreAPI.Dtos
{
    /// <summary>
    /// Product data to be persisted
    /// </summary>
    public class ProductWriteDto
    {
        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Wine glass</example>
        public string Name { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Crystal clear wine glasses are made from high quality glass</example>
        public string Description { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        /// <example>16.99</example>
        public double Price { get; set; }
    }
}