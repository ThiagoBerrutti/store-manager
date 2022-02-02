using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesAPI.Identity;
using System;
using System.Collections.Generic;

namespace SalesAPI.Persistence.EntityConfigurations
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    userRole => userRole
                            .HasOne(ur => ur.Role)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.RoleId)
                            .IsRequired(),

                    userRole => userRole
                            .HasOne(ur => ur.User)
                            .WithMany(u => u.UserRoles)
                            .HasForeignKey(ur => ur.UserId)
                            .IsRequired(),

                    userRole => userRole.HasKey(ur => new { ur.UserId, ur.RoleId })
                );

            var hasher = new PasswordHasher<User>();
            builder.HasData(new List<User>
            {
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null,"string"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },

                new User
                {
                    Id = 2,
                    UserName = "manager",
                    NormalizedUserName = "MANAGER",
                    PasswordHash = hasher.HashPassword(null,"string"),
                    SecurityStamp = Guid.NewGuid().ToString()

                },

                new User
                {
                    Id = 3,
                    UserName = "stock",
                    NormalizedUserName = "STOCK",
                    PasswordHash = hasher.HashPassword(null,"string"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },

                new User
                {
                    Id = 4,
                    UserName = "seller",
                    NormalizedUserName = "SELLER",
                    PasswordHash = hasher.HashPassword(null,"string"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                }
            });
        }
    }
}