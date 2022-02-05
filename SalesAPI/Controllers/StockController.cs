using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductStockController : Controller
    {
        private readonly IStockService _stockService;

        public ProductStockController(IStockService stockService)
        {
            _stockService = stockService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStockReadDto>>> GetAllStocks()
        {
            var result = await _stockService.GetAllDtoAsync();
            return Ok(result);
        }


        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult<ProductStockReadDto>> GetStockByProductId(int id)
        {
            var productStock = await _stockService.GetDtoByProductId(id);
            return Ok(productStock);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductStockReadDto>> GetStockById(int productId)
        {
            var productStock = await _stockService.GetDtoByProductId(productId);
            return Ok(productStock);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] ProductStockWriteDto stockUpdate)
        {
            var productStockDto = await _stockService.Update(id, stockUpdate);

            return Ok(productStockDto);
        }


        [Authorize(Roles = "Administrator,Manager,Stock")]
        [HttpPut("{id:int}/add")]
        public async Task<IActionResult> AddToStock(int id, int amount)
        {
            var productStockDto = await _stockService.AddProductAmount(id, amount);
            return Ok(productStockDto);
        }


        [HttpPut("{id:int}/remove")]
        public async Task<IActionResult> RemoveFromStock(int id, int amount)
        {
            var productStockDto = await _stockService.RemoveProductAmount(id, amount);
            return Ok(productStockDto);
        }
    }
}