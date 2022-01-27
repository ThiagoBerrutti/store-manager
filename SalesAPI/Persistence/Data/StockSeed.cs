using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesAPI.Persistence.Data
{
    public class StockSeed
    {
        private SalesDbContext _context;

        public StockSeed(SalesDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.ProductStocks.Any())
            {
                var stocks = new List<ProductStock>();
                var products = _context.Products.ToList();
                var random = new Random();

                foreach (Product p in products)
                {
                    stocks.Add(new ProductStock(p, random.Next(3000) / 100));
                }

                _context.ProductStocks.AddRange(stocks);
                _context.SaveChanges();
            }
        }
    }
}