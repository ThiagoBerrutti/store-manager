using SalesAPI.Exceptions;
using System.Collections.Generic;

namespace SalesAPI.Models
{
    public class Stock
    {
        private Dictionary<Product, int> _productsOnStock;

        public int this[Product product] => _productsOnStock[product];

        public void Add(Product product, int quantity)
        {
            if (_productsOnStock.ContainsKey(product))
            {
                _productsOnStock[product] += quantity;
                return;
            }

            _productsOnStock.Add(product, quantity);
        }

        public bool Contains(Product product)
        {
            return _productsOnStock.ContainsKey(product);
        }

        public void Remove(Product product, int quantity)
        {
            if (Contains(product))
            {
                if (_productsOnStock[product] - quantity < 0)
                {
                    throw new StockException("Not enough products on stock");
                }

                _productsOnStock[product] -= quantity;
            }
        }
    }
}