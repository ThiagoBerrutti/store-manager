using System.ComponentModel;

namespace StoreAPI.Dtos
{
    public class ProductParametersDto : QueryStringParameterDto
    {
        /// <summary>
        /// Only returns products with 'Price' greater than or equal to this.
        /// </summary>
        public double MinPrice { get; set; } = 0;

        /// <summary>
        /// Only returns products with 'Price' less than or equal to this.
        /// </summary>
        public double MaxPrice { get; set; } = double.MaxValue;

        /// <summary>
        /// Only returns products whose 'Name' contains this string (case insensitive)
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Only returns products whose 'Description' contains this string (case insensitive)
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Filters products by stock availability. <i>True</i> returns only products that are available on stock. <i>False</i> returns only out-of-stock products. <i>Null</i> returns all products.      
        /// </summary>
        public bool? OnStock { get; set; }
    }
}