using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Infra;
using StoreAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// Product stocks operations
    /// </summary>
    [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
    [ApiController]
    [Route("api/v1/stock")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }


        /// <summary>
        /// Finds all stocks of all products, filtering the result
        /// </summary>
        /// <remarks>The results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination", "json", AppConstants.Pagination.MetadataHeaderDescription)]
        [HttpGet]
        public async Task<IActionResult> GetAllStocksPaginated([FromQuery] StockParametersDto parameters)
        {
            var result = await _stockService.GetAllDtoPaginatedAsync(parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }


        /// <summary>
        /// Finds a product stock by the product's Id
        /// </summary>
        /// <remarks>Returns a single role</remarks>
        /// <param name="id">The role's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Product stock found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet("product")]
        public async Task<IActionResult> GetStockByProductId(int id)
        {
            var productStock = await _stockService.GetDtoByProductIdAsync(id);

            return Ok(productStock);
        }


        /// <summary>
        /// Finds a product stock by Id
        /// </summary>
        /// <remarks>Returns a single role</remarks>
        /// <param name="id">The role's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Product stock found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = nameof(GetStockById))]
        public async Task<IActionResult> GetStockById(int id)
        {
            var productStock = await _stockService.GetDtoByIdAsync(id);

            return Ok(productStock);
        }
        /// <summary>
        /// Updates an existing product stock
        /// </summary>
        /// <remarks>Use it to fix stock quantity inconsistencies and to update data. </remarks>
        /// <param name="id">Product stock's Id</param>
        /// <param name="stockUpdate">Product stock's updated data</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Stock updated")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] ProductStockWriteDto stockUpdate)
        {
            var productStockDto = await _stockService.UpdateAsync(id, stockUpdate);

            return Ok(productStockDto);
        }

        /// <summary>
        /// Adds quantity to a product stock
        /// </summary>
        /// <remarks>Use this on daily stock movement (i.e. products being returned, purchasing products from supplier, etc)</remarks>
        /// <param name="id">Product stock's Id</param>
        /// <param name="quantity">Quantity to add</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Added product quantity")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator,Manager,Stock")]
        [HttpPut("{id}/add/{quantity}")]
        public async Task<IActionResult> AddQuantityToStock(int id, int quantity)
        {
            var productStockDto = await _stockService.AddProductQuantityAsync(id, quantity);

            return Ok(productStockDto);
        }

        /// <summary>
        /// Adds quantity to a product stock
        /// </summary>
        /// <remarks>Use this on daily stock movement (i.e. selling)</remarks>
        /// <param name="id">Product stock's Id</param>
        /// <param name="quantity">Quantity to add</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Removed product quantity")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product stock not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}/remove/{quantity}")]
        public async Task<IActionResult> RemoveQuantityFromStock(int id, int quantity)
        {
            var productStockDto = await _stockService.RemoveProductQuantityAsync(id, quantity);

            return Ok(productStockDto);
        }
    }
}