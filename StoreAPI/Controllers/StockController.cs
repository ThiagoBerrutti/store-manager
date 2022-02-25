using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Dtos;
using StoreAPI.Dtos.Shared;
using StoreAPI.Services;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [Authorize(Roles = "Administrator,Manager,Stock,Seller")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }



        [HttpGet]
        public async Task<ActionResult<PagedList<ProductStockReadDto>>> GetAllStocksPaged([FromQuery] StockParametersDto parameters)
        {
            var result = await _stockService.GetAllDtoPagedAsync(parameters);
            var metadata = result.GetMetadata();

            Response.Headers.Add("X-Pagination", metadata);

            return Ok(result.Items);
        }


        [HttpGet("product")]
        public async Task<ActionResult<ProductStockReadDto>> GetStockByProductId(int id)
        {
            var productStock = await _stockService.GetDtoByProductIdAsync(id);

            return Ok(productStock);
        }


        [HttpGet("{id}", Name = nameof(GetStockById))]
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