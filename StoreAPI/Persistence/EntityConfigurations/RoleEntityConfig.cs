using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System.Collections.Generic;

namespace StoreAPI.Persistence.EntityConfigurations
{
    public class RoleEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new List<Role>
            {
                new Role
                {
                    Id = AppConstants.Roles.Admin.Id,
                    Name = AppConstants.Roles.Admin.Name,
                    NormalizedName = AppConstants.Roles.Admin.NormalizedName
                },

                new Role
                {
                    Id = AppConstants.Roles.Manager.Id,
                    Name = AppConstants.Roles.Manager.Name,
                    NormalizedName = AppConstants.Roles.Manager.NormalizedName
                },

                new Role
                {
                    Id = AppConstants.Roles.Stock.Id,
                    Name = AppConstants.Roles.Stock.Name,
                    NormalizedName = AppConstants.Roles.Stock.NormalizedName
                },

                new Role
                {
                    Id = AppConstants.Roles.Seller.Id,
                    Name = AppConstants.Roles.Seller.Name,
                    NormalizedName = AppConstants.Roles.Seller.NormalizedName
                }
            });
        }
    }
}