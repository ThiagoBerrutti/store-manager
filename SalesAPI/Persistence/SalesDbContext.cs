using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Identity;
using SalesAPI.Models;
using System.Collections.Generic;

namespace SalesAPI.Persistence
{
    public class SalesDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                    UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                    IdentityUserToken<int>>
    {
        public SalesDbContext()
        {
        }

        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<UserRole>(userRole =>
            //{
            //    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            //    userRole
            //        .HasOne(ur => ur.User)
            //        .WithMany(u => u.UserRoles)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();

            //    userRole
            //        .HasOne(ur => ur.Role)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<Role>(role =>
            //{
            //    role.HasData(new List<Role>
            //    {
            //        new Role
            //        {
            //            Id = 1,
            //            Name = "Administrator",
            //            NormalizedName = "ADMINISTRATOR"
            //        },

            //        new Role
            //        {
            //            Id = 2,
            //            Name = "Manager",
            //            NormalizedName = "MANAGER"
            //        },

            //        new Role
            //        {
            //            Id = 3,
            //            Name = "Stock",
            //            NormalizedName = "STOCK"
            //        },

            //        new Role
            //        {
            //            Id = 4,
            //            Name = "Seller",
            //            NormalizedName = "SELLER"
            //        }
            //    });
            //});

            //modelBuilder.Entity<User>(user =>
            //{
            //    user
            //        .HasMany(u => u.Roles)
            //        .WithMany(r => r.Users)
            //        .UsingEntity<UserRole>(
            //            userRole => userRole
            //                    .HasOne(ur => ur.Role)
            //                    .WithMany(r => r.UserRoles)
            //                    .HasForeignKey(ur => ur.RoleId)
            //                    .IsRequired(),

            //            userRole => userRole
            //                    .HasOne(ur => ur.User)
            //                    .WithMany(u => u.UserRoles)
            //                    .HasForeignKey(ur => ur.UserId)
            //                    .IsRequired(),

            //            userRole => userRole.HasKey(ur => new { ur.UserId, ur.RoleId })
            //        );

            //    user
            //        .HasData(new List<User>
            //        {
            //            new User
            //            {
            //                Id = 1,
            //                UserName = "admin",
            //                NormalizedUserName = "ADMIN"
            //            },

            //            new User
            //            {
            //                Id = 2,
            //                UserName = "managerUser",
            //                NormalizedUserName = "MANAGERUSER"
            //            },

            //            new User
            //            {
            //                Id = 3,
            //                UserName = "stockUser",
            //                NormalizedUserName = "MANAGERUSER"
            //            },

            //            new User
            //            {
            //                Id = 4,
            //                UserName = "sellerUser",
            //                NormalizedUserName = "MANAGERUSER"
            //            },
            //        });
            //});

            //modelBuilder.Entity<UserRole>(userRole =>
            //{
            //    userRole
            //        .HasData(new List<UserRole>
            //        {
            //            new UserRole { RoleId = 1, UserId = 1 },
            //            new UserRole { RoleId = 2, UserId = 2 },
            //            new UserRole { RoleId = 3, UserId = 3 },
            //            new UserRole { RoleId = 4, UserId = 4 }
            //        });
            //});

            modelBuilder.Entity<ProductStock>(productStock =>
            {
                productStock
                    .HasOne(s => s.Product)
                    .WithOne(p => p.ProductStock)
                    .HasForeignKey<ProductStock>(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(product =>
            {
                product
                    .HasOne(p => p.ProductStock)
                    .WithOne(ps => ps.Product);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        }
    }
}