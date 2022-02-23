using StoreAPI.Dtos;
using StoreAPI.Models;
using StoreAPI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Data
{
    public class ProductSeed
    {
        private readonly StoreDbContext _context;
        private readonly IProductService _productService;


        public ProductSeed(StoreDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }


        public async Task Seed()
        {
            if (!_context.Products.Any())
            {
                await _productService.CreateAsync(new ProductWriteDto { Name = "Abacate", Price = 9.99 , Description = "Abacate 1kg"});
                await _productService.CreateAsync(new ProductWriteDto { Name = "Berinjela", Price = 3.00, Description = "Berinjela preta 1kg"});
                await _productService.CreateAsync(new ProductWriteDto { Name = "Coco", Price = 7.50, Description = "Coco seco un" });
                await _productService.CreateAsync(new ProductWriteDto { Name = "Danoninho", Price = 6.00, Description = "Danoninho Ice 70g" });
                await _productService.CreateAsync(new ProductWriteDto { Name = "Espaguete", Price = 4.00, Description = "Espaguete Isabela 500g" });
            }
        }
    }
}