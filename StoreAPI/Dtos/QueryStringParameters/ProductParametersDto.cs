namespace StoreAPI.Dtos
{
    /// <summary>
    /// Query string parameters for product search filtering
    /// </summary>
    public class ProductParametersDto : QueryStringParameterDto
    {
        /// <summary>
        /// Only returns products with 'Price' greater than or equal to this.
        /// </summary>
        public double? MinPrice { get; set; }

        /// <summary>
        /// Only returns products with 'Price' less than or equal to this.
        /// </summary>
        public double? MaxPrice { get; set; }

        /// <summary>
        /// Only returns products whose 'Name' contains this string (case insensitive)
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Only returns products whose 'Description' contains this string (case insensitive)
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Filters products by stock availability.
        /// - **True**:  returns only products that are available on stock.
        /// - **False**: returns only out-of-stock products.
        /// </summary>
        public bool? OnStock { get; set; }
    }
}