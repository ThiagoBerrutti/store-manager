using Microsoft.EntityFrameworkCore;
using StoreAPI.Domain;
using StoreAPI.Dtos;
using StoreAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        
        public async Task<PagedList<Product>> GetAllWherePagedAsync(int pageNumber, int pageSize, Expression<Func<Product, bool>> expression)
        {
            var result = await _context.Products
                 .Include(p => p.ProductStock)
                 .OrderBy(p => p.Name)
                 .ThenBy(p => p.Id)
                 .Where(expression)
                 .ToPagedListAsync(pageNumber, pageSize);

            return result;
        }

        
        public async Task<PagedList<Product>> GetAllWithParameters(int pageNumber, int pageSize, int minPrice, int maxPrice, string name, string description, bool onStock)
        {
            var queryable = _context.Products
                .Include(p => p.ProductStock)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id);

            var result = await PagedList<Product>.ToPagedListAsync(queryable, pageNumber, pageSize);

            return result;

        }

        public async Task<PagedList<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            var items = _context.Products
                .Include(p => p.ProductStock)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id);

            var result = await PagedList<Product>.ToPagedListAsync(items, pageNumber, pageSize);

            return result;
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