using StoreAPI.Infra;

namespace StoreAPI.Dtos.Shared
{
    public class StockParametersDto : QueryStringParameterDto
    {
        public string ProductName { get; set; } = "";
        public int MinCount{ get; set; } = AppConstants.Validations.Stock.CountMinValue;
        public int MaxCount { get; set; } = AppConstants.Validations.Stock.CountMaxValue;
    }
}