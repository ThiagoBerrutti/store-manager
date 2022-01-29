using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAPI.Models;

namespace SalesAPI.Persistence.FluentApi
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}