using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly SalesDbContext _context;

        public ProductStockRepository(SalesDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ProductStock>> GetAll()
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .ToListAsync();
        }


        public async Task<ProductStock> GetByProductIdAsync(int productId)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.ProductId == productId);
        }


        public async Task<ProductStock> GetByIdAsync(int stockId)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.Id == stockId);
        }


        public async Task<ProductStock> GetByProductAsync(Product product)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.Product == product);
        }


        public void Create(ProductStock productStock)
        {
            _context.ProductStocks.Add(productStock);
        }


        public void Update(ProductStock productStock)
        {
            _context.ProductStocks.Update(productStock);
        }


        public void Delete(ProductStock productStock)
        {
            _context.ProductStocks.Remove(productStock);
        }        
    }
}