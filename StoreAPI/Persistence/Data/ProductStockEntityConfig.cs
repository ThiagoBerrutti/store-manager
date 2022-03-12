using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Domain;
using System.Collections.Generic;

namespace StoreAPI.Persistence.Data
{
    public class ProductStockEntityConfig : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.HasData(new List<ProductStock>
            {
                new ProductStock
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 200
                },

                new ProductStock
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 100
                },

                new ProductStock
                {
                    Id = 3,
                    ProductId = 3,
                    Quantity = 50
                },

                new ProductStock
                {
                    Id = 4,
                    ProductId = 4,
                    Quantity = 150
                },

                new ProductStock
                {
                    Id = 5,
                    ProductId = 5,
                    Quantity = 200
                }
            });
        }
    }
}