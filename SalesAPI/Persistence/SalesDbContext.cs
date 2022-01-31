using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;

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
        //public DbSet<Role> EmployeeRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                userRole
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(user =>
            {
                user
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
                });

            modelBuilder.Entity<Role>(role =>
            {
                role
                    .HasMany(r => r.Users)
                    .WithMany(u => u.Roles)
                    .UsingEntity<UserRole>(
                            userRole => userRole
                                    .HasOne(ur => ur.User)
                                    .WithMany(u => u.UserRoles)
                                    .HasForeignKey(ur => ur.UserId)
                                    .IsRequired(),

                            userRole => userRole
                                    .HasOne(ur => ur.Role)
                                    .WithMany(r => r.UserRoles)
                                    .HasForeignKey(ur => ur.RoleId)
                                    .IsRequired(),

                            userRole => userRole.HasKey(ur => new { ur.UserId, ur.RoleId })
                        );
            });

            modelBuilder.Entity<ProductStock>(stock =>
            {
                stock
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

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        }
    }
}