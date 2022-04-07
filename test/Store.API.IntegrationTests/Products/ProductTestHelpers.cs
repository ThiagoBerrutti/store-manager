using Microsoft.EntityFrameworkCore;
using Store.API.IntegrationTests.Stock;
using StoreAPI.Domain;
using StoreAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.API.IntegrationTests.Products
{
    public class ProductTestHelpers
    {
        public HttpClient Client { get; set; }
        public StoreDbContext Context { get; set; }

        public ProductTestHelpers(HttpClient client, StoreDbContext context)
        {
            Client = client;
            Context = context;
        }


        public async Task<List<Product>> CreateNewProductsAsync(int quantity = 1)
        {
            var products = new List<Product>();
            var productStocks = new List<ProductStock>();

            for (int i = 0; i < quantity; i++)
            {
                var productToCreate = ProductObjects.Factory.GenerateProduct();

                var productStockToCreate = ProductStockTestHelpers.Factory.GenerateProductStock(productToCreate);

                products.Add(productToCreate);
                productStocks.Add(productStockToCreate);
            }

            Context.Products.AddRange(products);
            Context.ProductStocks.AddRange(productStocks);

            await Context.SaveChangesAsync();

            return products;
        }


        public async Task<Product> CreateNewProductAsync() 
            => (await CreateNewProductsAsync()).FirstOrDefault();


        public async Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> expression) 
            => await Context.Products
                    .AsNoTracking()
                    .Where(expression)
                    .ToListAsync();
        

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> expression) 
            => await Context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(expression);


        public async Task<Product> GetProductWithFKAsync(Expression<Func<Product, bool>> expression) 
            => await Context.Products
                    .AsNoTracking()
                    .Include(p => p.ProductStock)
                    .FirstOrDefaultAsync(expression);
    }
}