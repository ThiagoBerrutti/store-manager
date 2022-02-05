using SalesAPI.Dtos;
using SalesAPI.Models;
using SalesAPI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Data
{
    public class ProductSeed
    {
        private readonly SalesDbContext _context;
        private readonly IProductService _productService;


        public ProductSeed(SalesDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }


        public async Task Seed()
        {
            if (!_context.Products.Any())
            {
                await _productService.CreateAsync(new ProductWriteDto { Name = "Abacate", Price = 9.99 });
                await _productService.CreateAsync(new ProductWriteDto { Name = "Berinjela", Price = 3.00 });
                await _productService.CreateAsync(new ProductWriteDto { Name = "Coco", Price = 5.50 });
                await _productService.CreateAsync(new ProductWriteDto { Name = "Danoninho", Price = 6.00 });
                await _productService.CreateAsync(new ProductWriteDto { Name = "Espaguete", Price = 4.00 });
            }
        }
    }
}