using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Persistence.Data;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        private readonly ProductSeed _seed;

        public ProductController(IProductService productService, ProductSeed seed)
        {
            _productService = productService;
            _seed = seed;
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product)
        {
            await _productService.CreateAsync(product);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductWriteDto productUpdate)
        {
            await _productService.UpdateAsync(id, productUpdate);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            await _productService.Clear();
            return Ok();
        }

        [HttpPost("seed")]
        public IActionResult Seed()
        {
            _seed.Seed();
            return Ok("Product seeded");
        }
    }
}