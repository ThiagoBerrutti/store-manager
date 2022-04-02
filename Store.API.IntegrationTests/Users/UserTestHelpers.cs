using Microsoft.EntityFrameworkCore;
using Store.API.IntegrationTests.Auth;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using StoreAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.API.IntegrationTests.Users
{
    public class UserTestHelpers
    {
        public HttpClient Client { get; set; }
        public StoreDbContext Context { get; set; }

        public UserTestHelpers(HttpClient client, StoreDbContext context)
        {
            Client = client;
            Context = context;
        }

        public async Task<List<User>> GetUsersAsync(Expression<Func<User, bool>> expression)
            => await Context.Users
                .AsNoTracking()
                .Where(expression)
                .ToListAsync();

        public async Task<List<User>> GetUsersAsync()
            => await Context.Users
                .AsNoTracking()
                .ToListAsync();

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> expression)
            => await Context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);


        public async Task<List<User>> CreateNewUsersAsync(int count = 1)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                var user = UserObjects.Factory.GenerateUser();
                users.Add(user);
            }

            Context.Users.AddRange(users);
            await Context.SaveChangesAsync();

            return users;
        }


        public async Task<User> CreateNewUserAsync()
        {
            var user = UserObjects.Factory.GenerateUser();
            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            return user;
        }


        public async Task AddRolesToUser(int userId, IEnumerable<int> roleIds)
        {
            var rolesOnDbIds = Context.Roles.Select(r => r.Id);
            roleIds = roleIds.Intersect(rolesOnDbIds);

            foreach (var roleId in roleIds)
            {
                if (await Context.UserRoles.FindAsync(userId, roleId) is null)
                {
                    var userRole = new UserRole { UserId = userId, RoleId = roleId };
                    Context.UserRoles.Add(userRole);
                }
            }

            await Context.SaveChangesAsync();
        }


        


        public static string CreateFullName(string firstName, string lastName) => firstName + " " + lastName;

        public static string NumbersInString(string name) => Regex.Replace(name, @"[\D]", "");
        //public static string SelectNumbersFromUserName(string name) => Regex.Replace(name, @"^\d$", "");

    }
}