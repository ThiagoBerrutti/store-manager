using StoreAPI.Infra;

namespace StoreAPI.Dtos
{
    public abstract class QueryStringParameterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = AppConstants.QueryString.DefaultPageSize;
    }
}