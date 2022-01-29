using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAPI.Models;

namespace SalesAPI.Persistence.FluentApi
{
    public class StockEntityTypeConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder
                .HasOne(s => s.Product)
                .WithOne(p => p.ProductStock)
                .HasForeignKey<ProductStock>(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}