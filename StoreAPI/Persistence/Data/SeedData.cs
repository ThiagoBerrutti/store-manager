using Microsoft.AspNetCore.Identity;
using StoreAPI.Domain;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreAPI.Persistence.Data
{
    public static class SeedData
    {
        public static readonly ReadOnlyCollection<Product> Products = new ReadOnlyCollection<Product>
            (new List<Product> {
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

        public static readonly ReadOnlyCollection<ProductStock> ProductStocks = new ReadOnlyCollection<ProductStock>
            (new List<ProductStock>
            {
                new ProductStock
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 200
                },

                new ProductStock
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 100
                },

                new ProductStock
                {
                    Id = 3,
                    ProductId = 3,
                    Quantity = 50
                },

                new ProductStock
                {
                    Id = 4,
                    ProductId = 4,
                    Quantity = 150
                },

                new ProductStock
                {
                    Id = 5,
                    ProductId = 5,
                    Quantity = 200
                }
            });

        public static readonly ReadOnlyCollection<Role> Roles = new ReadOnlyCollection<Role>
            (new List<Role>{
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

        
        public static readonly ReadOnlyCollection<User> Users = new ReadOnlyCollection<User> 
         (new List<User>
            {
                //Admin
                new User
                {
                    Id = AppConstants.Users.Admin.Id,
                    UserName = AppConstants.Users.Admin.UserName,
                    NormalizedUserName = AppConstants.Users.Admin.NormalizedUserName,
                    FirstName = AppConstants.Users.Admin.FirstName,
                    LastName = AppConstants.Users.Admin.LastName,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null,AppConstants.Users.Admin.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Admin.DateOfBirth)
    },

                //Manager
                new User
                {
                    Id = AppConstants.Users.Manager.Id,
                    UserName = AppConstants.Users.Manager.UserName,
                    NormalizedUserName = AppConstants.Users.Manager.NormalizedUserName,
                    FirstName = AppConstants.Users.Manager.FirstName,
                    LastName = AppConstants.Users.Manager.LastName,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null,AppConstants.Users.Manager.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Manager.DateOfBirth)
},

                //Stock
                new User
                {
                    Id = AppConstants.Users.Stock.Id,
                    UserName = AppConstants.Users.Stock.UserName,
                    NormalizedUserName = AppConstants.Users.Stock.NormalizedUserName,
                    FirstName = AppConstants.Users.Stock.FirstName,
                    LastName = AppConstants.Users.Stock.LastName,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, AppConstants.Users.Stock.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Stock.DateOfBirth)
                },

                //Seller
                new User
                {
                    Id = AppConstants.Users.Seller.Id,
                    UserName = AppConstants.Users.Seller.UserName,
                    NormalizedUserName = AppConstants.Users.Seller.NormalizedUserName,
                    FirstName = AppConstants.Users.Seller.FirstName,
                    LastName = AppConstants.Users.Seller.LastName,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, AppConstants.Users.Seller.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Seller.DateOfBirth)
                },

                //Public
                new User
                {
                    Id = AppConstants.Users.Public.Id,
                    UserName = AppConstants.Users.Public.UserName,
                    NormalizedUserName = AppConstants.Users.Public.NormalizedUserName,
                    FirstName = AppConstants.Users.Public.FirstName,
                    LastName = AppConstants.Users.Public.LastName,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, AppConstants.Users.Public.Password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    DateOfBirth = DateTime.Parse(AppConstants.Users.Public.DateOfBirth)
                }
            });

        public static readonly ReadOnlyCollection<UserRole> UserRoles = new ReadOnlyCollection<UserRole> 
            (new List<UserRole>
            {
                new UserRole { RoleId = 1, UserId = 1 },
                new UserRole { RoleId = 2, UserId = 2 },
                new UserRole { RoleId = 3, UserId = 3 },
                new UserRole { RoleId = 4, UserId = 4 }
            });
    }
}