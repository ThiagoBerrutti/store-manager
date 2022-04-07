using Microsoft.EntityFrameworkCore;
using StoreAPI.Identity;
using StoreAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.API.IntegrationTests.Roles
{
    public class RoleTestHelpers
    {
        public HttpClient Client { get; set; }
        public StoreDbContext Context { get; set; }

        public RoleTestHelpers(HttpClient client, StoreDbContext context)
        {
            Client = client;
            Context = context;
        }

        public async Task<List<Role>> CreateNewRolesAsync(int quantity = 1)
        {
            var roles = new List<Role>();

            for (int i = 0; i < quantity; i++)
            {
                var roleToCreate = RoleObjects.Factory.GenerateRole();

                roles.Add(roleToCreate);
            }

            Context.Roles.AddRange(roles);

            await Context.SaveChangesAsync();

            return roles;
        }


        public async Task<Role> CreateNewRoleAsync()
            => (await CreateNewRolesAsync()).FirstOrDefault();


        public async Task AddUsersToRole(int roleId, IEnumerable<int> userIds)
        {
            var users = Context.Users.Where(u => userIds.Contains(u.Id));

            foreach (var u in users)
            {
                if (!Context.UserRoles.Any(ur => ur.RoleId == roleId && ur.UserId == u.Id))
                {
                    var userRole = new UserRole { UserId = u.Id, RoleId = roleId };
                    Context.UserRoles.Add(userRole);
                }
            }

            await Context.SaveChangesAsync();
        }


        public async Task<Role> GetRoleAsync(Expression<Func<Role, bool>> expression)
            => await Context.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(expression);
    }
}