using Microsoft.AspNetCore.Mvc;
using SalesAPI.Services;
using SalesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [ApiController]
    [Route("api/v1/stock")]
    public class ProductStockController : Controller
    {
        private IStockService _stockService;

        public ProductStockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _stockService.GetAllProducts());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] int productId, [FromQuery] int quantity)
        {
            await _stockService.AddProduct(productId, quantity);
            return Ok();
        }
    }
}
