using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                                .OrderBy(p => p.Name)
                                .Include(p => p.ProductStock)
                                .ToListAsync();
        }


        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                .Include(p => p.ProductStock)
                                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<Product>> SearchAsync(string search)
        {
            return await _context.Products
                                    .Include(p => p.ProductStock)
                                    .Where(p =>
                                        p.Name.ToLower().Contains(search) ||
                                        p.Description.ToLower().Contains(search))
                                    .ToListAsync();
        }


        public void Add(Product product)
        {
            _context.Products.Add(product);
        }


        public void Update(Product product)
        {
            _context.Products.Update(product);
        }


        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}