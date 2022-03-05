using StoreAPI.Infra;
using System.ComponentModel;

namespace StoreAPI.Dtos
{
    public class QueryStringParameterDto
    {
        /// <summary>
        /// The page number to show from a paginated result
        /// 
        /// <i>(Default: 1)</i>
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of items per page
        /// 
        /// <i>(Default: 10)</i>
        /// </summary>
        //[DefaultValue(AppConstants.Pagination.DefaultPageSize)]
        public int PageSize { get; set; } = AppConstants.Pagination.DefaultPageSize;
    }
}