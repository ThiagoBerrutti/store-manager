namespace StoreAPI.Dtos
{
    public class ProductParametersDto : QueryStringParameterDto
    {
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public bool OnStock { get; set; } = true;
    }
}