namespace StoreAPI.Dtos
{
    public class ProductParametersDto : QueryStringParameterDto
    {
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = double.MaxValue;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public bool OnStock { get; set; } = true;
    }
}