using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAPI.Models;

namespace SalesAPI.Persistence.FluentApi
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder
            //    .HasOne(e => e.Role)
            //    .WithMany(p => p.Employees)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}