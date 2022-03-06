using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Services;
using StoreAPI.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// Operations with the list of products the store works
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/products")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    //[ProducesErrorResponseType(typeof(void))]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        /// <summary>
        /// Finds all products, filtering the result
        /// </summary>
        /// <remarks>Results are paginated. To configure pagination, include the query string parameters 'pageSize' and 'pageNumber'</remarks>
        /// <param name="parameters">Query string with the result filters and pagination values</param>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<ProductReadDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-Pagination","string", Descriptions.XPaginationDescription)]
        [HttpGet(Name = nameof(GetAllProductsPaginated))]
        public async Task<IActionResult> GetAllProductsPaginated([FromQuery] ProductParametersDto parameters)
        {
            var result = await _productService.GetAllDtoPaginatedAsync(parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }


        /// <summary>
        /// Finds an product by Id
        /// </summary>
        /// <remarks>Returns a single product</remarks>
        /// <param name="id">The product's Id</param>
        [SwaggerResponse(StatusCodes.Status200OK, "Product found")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var result = await _productService.GetDtoByIdAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <remarks>Create a new product and a new product stock for it, with the initial quantity informed</remarks>
        /// <param name="product">Product object to be add to store</param>
        /// <param name="quantity">Initial quantity of product in stock</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status201Created, "Product created", typeof(ProductReadWithStockDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(CreateProduct))]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product, int quantity)
        {
            var productCreated = await _productService.CreateAsync(product, quantity);
            return CreatedAtRoute(nameof(GetProductById), new { productCreated.Id }, productCreated);
        }


        /// <summary>
        /// Delete an existing product
        /// </summary>
        /// <param name="id">The product Id</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        [HttpDelete("{id}", Name = nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }


        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Product's Id</param>
        /// <param name="productUpdate">Product's updated data</param>
        [Authorize(Roles = "Administrator,Manager")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product updated", typeof(ProductWriteDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}", Name = nameof(UpdateProduct))]
        public async Task<IActionResult> UpdateProduct(int id, ProductWriteDto productUpdate)
        {
            var updatedProductDto = await _productService.UpdateAsync(id, productUpdate);
            return Ok(updatedProductDto);
        }
    }
}