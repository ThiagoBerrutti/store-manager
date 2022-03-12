using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Domain;
using System.Collections.Generic;

namespace StoreAPI.Persistence.Data
{
    public class ProductEntityConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Abacate",
                    Price = 9.99,
                    Description = "Abacate 1kg"
                },

                new Product
                {
                    Id = 2,
                    Name = "Berinjela",
                    Price = 3.00,
                    Description = "Berinjela preta 1kg"
                },

                new Product
                {
                    Id = 3,
                    Name = "Coco",
                    Price = 7.50,
                    Description = "Coco seco un"
                },

                new Product
                {
                    Id = 4,
                    Name = "Danoninho",
                    Price = 6.00,
                    Description = "Danoninho Ice 70g"
                },

                new Product
                {
                    Id = 5,
                    Name = "Espaguete",
                    Price = 4.00,
                    Description = "Espaguete Isabela 500g"
                }
            });
        }
    }
}