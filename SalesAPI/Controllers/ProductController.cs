using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
        {
            var a = await _productService.GetAllDtoAsync();
            return Ok(a);
        }


        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var result = await _productService.GetDtoByIdAsync(id);
            return Ok(result);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product)
        {
            var productCreated = await _productService.CreateAsync(product);
            return CreatedAtRoute(nameof(GetProductById),productCreated.Id,productCreated);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductWriteDto productUpdate)
        {
            await _productService.UpdateAsync(id, productUpdate);
            return Ok();
        }
    }
}