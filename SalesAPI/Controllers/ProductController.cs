using Microsoft.AspNetCore.Mvc;
using SalesAPI.Models;
using SalesAPI.Services;
using SalesAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var a = await _productService.GetAllAsync();
            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductWriteDto product)
        {
            await _productService.CreateProductAsync(product);
            return Ok();
        }
    }
}