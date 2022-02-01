using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Models;
using SalesAPI.Persistence.Data;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Controllers
{
    [Authorize(Roles = "Administrator,Manager,Stock,Sales")]
    [ApiController]
    [Route("api/v1/stock")]
    public class ProductStockController : Controller
    {
        private readonly IStockService _stockService;

        public ProductStockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockReadDto>>> GetAll()
        {
            var result = await _stockService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<StockReadDto>> GetByProductId([FromRoute] int productId)
        {
            var productStock = await _stockService.GetByProductId(productId);
            return Ok(productStock);
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] StockWriteDto stockUpdate)
        {
            await _stockService.Update(id, stockUpdate);

            return Ok();
        }

        [Authorize(Roles = "Administrator,Manager,Stock")]
        [HttpPut("{id:int}/add")]
        public async Task<IActionResult> AddToStock(int id, int amount)
        {
            await _stockService.AddProductAmount(id, amount);
            return Ok();
        }


        [HttpPut("{id:int}/remove")]
        public async Task<IActionResult> RemoveFromStock(int id, int amount)
        {
            await _stockService.RemoveProductAmount(id, amount);
            return Ok();
        }
    }
}