using StoreAPI.Infra;

namespace StoreAPI.Dtos
{
    public class StockParametersDto : QueryStringParameterDto
    {
        public string ProductName { get; set; } = "";
        public int QuantityMin { get; set; } = AppConstants.Validations.Stock.QuantityMinValue;
        public int QuantityMax { get; set; } = AppConstants.Validations.Stock.QuantityMaxValue;
    }
}