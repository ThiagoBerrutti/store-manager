using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Identity;
using System.Collections.Generic;

namespace StoreAPI.Persistence.Data
{
    public class UserRoleEntityConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(new List<UserRole>
            {
                new UserRole { RoleId = 1, UserId = 1 },
                new UserRole { RoleId = 2, UserId = 2 },
                new UserRole { RoleId = 3, UserId = 3 },
                new UserRole { RoleId = 4, UserId = 4 }
            });
        }
    }
}