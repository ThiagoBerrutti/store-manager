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
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStockReadDto>>> GetAllStocks()
        {
            var result = await _stockService.GetAllDtoAsync();
            return Ok(result);
        }


        [HttpGet("{productId:int}")]
        public async Task<ActionResult<ProductStockReadDto>> GetStockByProductId(int productId)
        {
            var productStock = await _stockService.GetDtoByProductIdAsync(productId);
            return Ok(productStock);
        }

        
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{productId:int}")]
        public async Task<IActionResult> UpdateStock(int productId, [FromBody] ProductStockWriteDto stockUpdate)
        {
            var productStockDto = await _stockService.UpdateAsync(productId, stockUpdate);

            return Ok(productStockDto);
        }


        [Authorize(Roles = "Administrator,Manager,Stock")]
        [HttpPut("{productId:int}/add")]
        public async Task<IActionResult> AddToStock(int productId, int amount)
        {
            var productStockDto = await _stockService.AddProductAmountAsync(productId, amount);
            return Ok(productStockDto);
        }


        [HttpPut("{productId:int}/remove")]
        public async Task<IActionResult> RemoveFromStock(int productId, int amount)
        {
            var productStockDto = await _stockService.RemoveProductAmountAsync(productId, amount);
            return Ok(productStockDto);
        }
    }
}