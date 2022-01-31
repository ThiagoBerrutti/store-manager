using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAPI.Models;

namespace SalesAPI.Persistence.FluentApi
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder
            //    .HasOne(p => p.ProductStock)
            //    .WithOne(ps => ps.Product)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}