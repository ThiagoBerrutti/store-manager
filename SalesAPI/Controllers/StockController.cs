using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Dtos;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Linq;
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


        [HttpGet("product")]
        public async Task<ActionResult<ProductStockReadDto>> GetStockByProductId(int id)
        {
            var productStock = await _stockService.GetDtoByProductIdAsync(id);

            return Ok(productStock);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStockReadDto>> GetStockById(int id)
        {
            var productStock = await _stockService.GetDtoByIdAsync(id);

            return Ok(productStock);
        }


        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] ProductStockWriteDto stockUpdate)
        {
            var productStockDto = await _stockService.UpdateAsync(id, stockUpdate);

            return Ok(productStockDto);
        }


        [Authorize(Roles = "Administrator,Manager,Stock")]
        [HttpPut("{id}/add")]
        public async Task<IActionResult> AddAmountToStock(int id, int amount)
        {
            var productStockDto = await _stockService.AddProductAmountAsync(id, amount);

            return Ok(productStockDto);
        }


        [HttpPut("{id}/remove")]
        public async Task<IActionResult> RemoveAmountFromStock(int id, int amount)
        {
            var productStockDto = await _stockService.RemoveProductAmountAsync(id, amount);

            return Ok(productStockDto);
        }
    }
}