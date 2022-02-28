using Microsoft.EntityFrameworkCore;
using StoreAPI.Domain;
using StoreAPI.Dtos;
using StoreAPI.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StoreDbContext _context;

        public StockRepository(StoreDbContext context)
        {
            _context = context;
        }


        public async Task<PaginatedList<ProductStock>> GetAllWherePaginatedAsync(int pageNumber, int pageSize, Expression<Func<ProductStock, bool>> expression)
        {
            var result = await _context.ProductStocks
                .Include(s => s.Product)
                .OrderBy(s => s.Product.Name)
                .Where(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);

            return result;
        }


        public async Task<ProductStock> GetByProductIdAsync(int productId)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.ProductId == productId);
        }


        public async Task<ProductStock> GetByIdAsync(int id)
        {
            return await _context.ProductStocks
                                .Include(ps => ps.Product)
                                .FirstOrDefaultAsync(ps => ps.Id == id);
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