using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreAPI.Dtos;
using StoreAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
        //{
        //    var result = await _productService.GetAllDtoAsync();
        //    return Ok(result);
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<ActionResult<PagedList<ProductReadDto>>> GetAllProductsPaginated([FromQuery] ProductParametersDto parameters)
        //{
        //    var result = await _productService.GetAllDtoPaginatedAsync(parameters);
        //    var metadata = result.GetMetadata();

        //    Response.Headers.Add("X-Pagination", metadata);

        //    return Ok(result);
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagedList<ProductReadDto>>> GetAllProductsPaginated([FromQuery] ProductParametersDto parameters)
        {
            var result = await _productService.GetAllDtoPaginatedWithParameters(parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result);
        }


        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var result = await _productService.GetDtoByIdAsync(id);
            return Ok(result);
        }


        [HttpGet("search")]
        public async Task<ActionResult<ProductReadDto>> SearchProduct(string search)
        {
            var result = await _productService.SearchDtosAsync(search);
            return Ok(result);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product, int amount)
        {
            var productCreated = await _productService.CreateAsync(product, amount);
            return CreatedAtRoute(nameof(GetProductById), new { productCreated.Id }, productCreated);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{id}", Name = nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}", Name = nameof(UpdateProduct))]
        public async Task<IActionResult> UpdateProduct(int id, ProductWriteDto productUpdate)
        {
            var updatedProductDto = await _productService.UpdateAsync(id, productUpdate);
            return Ok(updatedProductDto);
        }
    }
}