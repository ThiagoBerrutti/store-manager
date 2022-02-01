using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{    
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
        {
            var a = await _productService.GetAllAsync();
            return Ok(a);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductReadDto>> GetById([FromRoute] int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product)
        {
            await _productService.CreateAsync(product);
            return Ok();
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductWriteDto productUpdate)
        {
            await _productService.UpdateAsync(id, productUpdate);
            return Ok();
        }
    }
}