using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System;
using System.Collections.Generic;

namespace StoreAPI.Persistence.Data
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(

                    builder => builder
                        .HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired(),


                    builder => builder
                        .HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired(),

                    builder => builder.HasKey(ur => new { ur.UserId, ur.RoleId })
                );

            var hasher = new PasswordHasher<User>();
            builder.HasData(SeedData.Users);
            //new List<User>
            //{
            //    //Admin
            //    new User
            //    {
            //        Id = AppConstants.Users.Admin.Id,
            //        UserName = AppConstants.Users.Admin.UserName,
            //        NormalizedUserName = AppConstants.Users.Admin.NormalizedUserName,
            //        FirstName = AppConstants.Users.Admin.FirstName,
            //        LastName = AppConstants.Users.Admin.LastName,
            //        PasswordHash = hasher.HashPassword(null,AppConstants.Users.Admin.Password),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        DateOfBirth = DateTime.Parse(AppConstants.Users.Admin.DateOfBirth)
            //    },

            //    //Manager
            //    new User
            //    {
            //        Id = AppConstants.Users.Manager.Id,
            //        UserName = AppConstants.Users.Manager.UserName,
            //        NormalizedUserName = AppConstants.Users.Manager.NormalizedUserName,
            //        FirstName = AppConstants.Users.Manager.FirstName,
            //        LastName = AppConstants.Users.Manager.LastName,
            //        PasswordHash = hasher.HashPassword(null,AppConstants.Users.Manager.Password),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        DateOfBirth = DateTime.Parse(AppConstants.Users.Manager.DateOfBirth)
            //    },

            //    //Stock
            //    new User
            //    {
            //        Id = AppConstants.Users.Stock.Id,
            //        UserName = AppConstants.Users.Stock.UserName,
            //        NormalizedUserName = AppConstants.Users.Stock.NormalizedUserName,
            //        FirstName = AppConstants.Users.Stock.FirstName,
            //        LastName = AppConstants.Users.Stock.LastName,
            //        PasswordHash = hasher.HashPassword(null,AppConstants.Users.Stock.Password),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        DateOfBirth = DateTime.Parse(AppConstants.Users.Stock.DateOfBirth)
            //    },

            //    //Seller
            //    new User
            //    {
            //        Id = AppConstants.Users.Seller.Id,
            //        UserName = AppConstants.Users.Seller.UserName,
            //        NormalizedUserName = AppConstants.Users.Seller.NormalizedUserName,
            //        FirstName = AppConstants.Users.Seller.FirstName,
            //        LastName = AppConstants.Users.Seller.LastName,
            //        PasswordHash = hasher.HashPassword(null,AppConstants.Users.Seller.Password),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        DateOfBirth = DateTime.Parse(AppConstants.Users.Seller.DateOfBirth)
            //    },

            //    //Public
            //    new User
            //    {
            //        Id = AppConstants.Users.Public.Id,
            //        UserName = AppConstants.Users.Public.UserName,
            //        NormalizedUserName = AppConstants.Users.Public.NormalizedUserName,
            //        FirstName = AppConstants.Users.Public.FirstName,
            //        LastName = AppConstants.Users.Public.LastName,
            //        PasswordHash = hasher.HashPassword(null,AppConstants.Users.Public.Password),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        DateOfBirth = DateTime.Parse(AppConstants.Users.Public.DateOfBirth)
            //    }
            //});
        }
    }
}