using StoreAPI.Infra;
using System.ComponentModel;

namespace StoreAPI.Dtos
{
    public class QueryStringParameterDto
    {
        /// <summary>
        /// <i>(Optional) </i>The page number to return from a paginated result
        /// </summary>
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// <i>(Optional) </i>Number of items per page<br/>
        /// </summary>
        [DefaultValue(AppConstants.Pagination.DefaultPageSize)]
        public int PageSize { get; set; } = AppConstants.Pagination.DefaultPageSize;
    }
}