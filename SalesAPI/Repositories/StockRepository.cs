using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;
using SalesAPI.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalesAPI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly SalesDbContext _context;

        public StockRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async IAsyncEnumerable<ProductStock> GetAll()
        {
            var a = _context.ProductStocks.Include(ps => ps.Product).AsAsyncEnumerable();
            await foreach(ProductStock ps in a)
            {
                yield return ps;
            }
        }

        public async Task<ProductStock> GetByIdAsync(int id)
        {
            return await _context.ProductStocks.FindAsync(id);
        }

        public async Task<ProductStock> GetByProductAsync(Product product)
        {
            foreach(ProductStock ps in _context.ProductStocks){
                var psProduct = ps.Product;
                if (psProduct == product) return await _context.ProductStocks.FindAsync(product.Id);
            }

            return null;
            //return await _context.ProductStocks.FirstOrDefaultAsync(ps => ps.Product == product);
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
