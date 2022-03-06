using StoreAPI.Infra;

namespace StoreAPI.Dtos
{
    /// <summary>
    /// Query string parameters for stock search filtering
    /// </summary>
    public class StockParametersDto : QueryStringParameterDto
    {
        /// <summary>
        /// Returns only stocks whose product contains this string
        /// </summary>
        public string ProductName { get; set; } = "";

        /// <summary>
        /// Returns only product stocks with quantity greater than or equal to this
        /// </summary>
        public int? QuantityMin { get; set; }

        /// <summary>
        /// Returns only product stocks with quantity less than or equal to this
        /// </summary>
        public int? QuantityMax { get; set; }
    }
}