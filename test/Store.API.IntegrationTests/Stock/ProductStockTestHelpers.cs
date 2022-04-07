using Microsoft.EntityFrameworkCore;
using StoreAPI.Domain;
using StoreAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.API.IntegrationTests.Stock
{
    public class ProductStockTestHelpers
    {
        public HttpClient Client { get; set; }
        public StoreDbContext Context { get; set; }

        public ProductStockTestHelpers(HttpClient client, StoreDbContext context)
        {
            Client = client;
            Context = context;
        }

        public static class Factory
        {
            public static ProductStock GenerateProductStock(Product product) => new ProductStock
            {
                Product = product,
                Quantity = new Random().Next(1000, 9999),
                ProductId = product.Id
            };
        }


        public async Task<List<ProductStock>> GetProductStocksAsync(Expression<Func<ProductStock, bool>> expression)
            => await Context.ProductStocks
                        .AsNoTracking()
                        .Where(expression)
                        .ToListAsync();


        public async Task<List<ProductStock>> GetProductStocksAsync()
             => await Context.ProductStocks
                        .AsNoTracking()
                        .ToListAsync();


        public async Task<ProductStock> GetProductStockAsync(Expression<Func<ProductStock, bool>> expression)
             => await Context.ProductStocks
                        .AsNoTracking()
                        .FirstOrDefaultAsync(expression);
    }
}