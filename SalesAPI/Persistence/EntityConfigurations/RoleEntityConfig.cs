using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAPI.Identity;
using System.Collections.Generic;

namespace SalesAPI.Persistence.EntityConfigurations
{
    public class RoleEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },

                new Role
                {
                    Id = 2,
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },

                new Role
                {
                    Id = 3,
                    Name = "Stock",
                    NormalizedName = "STOCK"
                },

                new Role
                {
                    Id = 4,
                    Name = "Seller",
                    NormalizedName = "SELLER"
                }
            });
        }
    }
}