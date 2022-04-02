using Microsoft.EntityFrameworkCore;
using StoreAPI;
using StoreAPI.Dtos;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests.Roles
{
    public class RoleControllerTests : TestBase, IAsyncLifetime
    {
        public RoleControllerTests(TestWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        //private async Task<List<RoleReadDto>> CreateNewRoleAsync(int quantity = 1)
        //{
        //    var uri = ApiRoutes.Roles.CreateRole;
        //    await AuthenticateAsAdminAsync();

        //    var result = new List<RoleReadDto>();

        //    for (int i = 0; i < quantity; i++)
        //    {
        //        var roleToCreate = RoleObjects.Factory.GenerateRoleWriteDto();

        //        var response = await Client.PostAsJsonAsync(uri, roleToCreate);
        //        var roleCreated = await response.Content.ReadAsAsync<RoleReadDto>();

        //        result.Add(roleCreated);
        //    }

        //    LogoutUser();

        //    return result;
        //}

        //private async Task AddUsersToRole(int roleId, IEnumerable<int> userIds)
        //{
        //    var users = Context.Users.Where(u => userIds.Contains(u.Id));

        //    foreach (var u in users)
        //    {
        //        if (!Context.UserRoles.Any(ur => ur.RoleId == roleId && ur.UserId == u.Id))
        //        {
        //            var userRole = new UserRole { UserId = u.Id, RoleId = roleId };
        //            Context.UserRoles.Add(userRole);
        //        }
        //    }

        //    await Context.SaveChangesAsync();

        //    LogoutUser();
        //}

        //private async Task<RoleReadDto> CreateNewRole(RoleWriteDto role)
        //{
        //    var uri = ApiRoutes.Roles.CreateRole;
        //    await AuthenticateAsAdminAsync();

        //    var response = await Client.PostAsJsonAsync(uri, role);
        //    var result = await response.Content.ReadAsAsync<RoleReadDto>();

        //    LogoutUser();

        //    return result;
        //}


        [Fact]
        public async Task GetAllRolesPaginated_ReturnsAllFourResults_WhenDatabaseIsReseted()
        {
            // Arrange
            var uri = ApiRoutes.Roles.GetAllRolesPaginated;
            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<List<RoleReadDto>>();

            // Assert
            var resultAdmin = result?.FirstOrDefault(p => p.Id == AppConstants.Roles.Admin.Id);
            var resultManager = result?.FirstOrDefault(p => p.Id == AppConstants.Roles.Manager.Id);
            var resultStock = result?.FirstOrDefault(p => p.Id == AppConstants.Roles.Stock.Id);
            var resultSeller = result?.FirstOrDefault(p => p.Id == AppConstants.Roles.Seller.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(AppConstants.Roles.Count, result.Count);
            Assert.NotNull(resultAdmin);
            Assert.NotNull(resultManager);
            Assert.NotNull(resultStock);
            Assert.NotNull(resultSeller);

            Assert.Equal(AppConstants.Roles.Admin.Name, resultAdmin.Name);
            Assert.Equal(AppConstants.Roles.Manager.Name, resultManager.Name);
            Assert.Equal(AppConstants.Roles.Stock.Name, resultStock.Name);
            Assert.Equal(AppConstants.Roles.Seller.Name, resultSeller.Name);
        }

        [Fact]
        public async Task GetRoleById_Returns_Role()
        {
            // Arrange
            var createdRole = await Helpers.Role.CreateNewRoleAsync();

            var roleId = createdRole.Id;
            var uri = ApiRoutes.Roles.GetRoleById.Replace("{id:int}", roleId.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var roleReadDto = await response.Content.ReadFromJsonAsync<RoleReadDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(roleReadDto);
            Assert.IsType<RoleReadDto>(roleReadDto);

            Assert.Equal(roleId, roleReadDto.Id);
            Assert.NotEmpty(roleReadDto.Name);
            Assert.Equal(createdRole.Name, roleReadDto.Name);
        }


        [Fact]
        public async Task GetRoleByName_SuccessfullyReturnsRole()
        {
            // Arrange
            var roleCreated = await Helpers.Role.CreateNewRoleAsync();
            var roleName = roleCreated.Name;

            var route = ApiRoutes.Roles.GetRoleByName;
            var uri = route.Replace("{roleName}", roleName);

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<RoleReadDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);

            Assert.Equal(roleName, result.Name);
            Assert.Equal(roleCreated.Id, result.Id);
        }


        [Fact]
        public async Task GetUsersOnRole_SuccessfullyReturnUsers()
        {
            // Arrange
            var roleCreated = await Helpers.Role.CreateNewRoleAsync();

            var userIds = new List<int>
            {
                AppConstants.Users.Manager.Id,
                AppConstants.Users.Seller.Id,
                AppConstants.Users.Public.Id
            };

            await Helpers.Role.AddUsersToRole(roleCreated.Id, userIds);

            var route = ApiRoutes.Roles.GetUsersOnRole;
            var uri = route.Replace("{id}", roleCreated.Id.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<List<UserReadDto>>();

            // Assert
            var userIdsOnResult = result.Select(u => u.Id);
            var intersect = userIds.Intersect(userIdsOnResult); 

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(roleCreated);
            Assert.Equal(userIds.Count, intersect.Count());

            Assert.Contains(AppConstants.Users.Manager.Id, userIdsOnResult);
            Assert.Contains(AppConstants.Users.Seller.Id, userIdsOnResult);
            Assert.Contains(AppConstants.Users.Public.Id, userIdsOnResult);
        }


        [Fact]
        public async Task CreateRole_SuccessfullyCreatesRole()
        {
            // Arrange
            var uri = ApiRoutes.Roles.CreateRole;
            var roleToCreate = RoleObjects.Factory.GenerateRoleWriteDto();

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PostAsJsonAsync(uri, roleToCreate);
            var result = await response.Content.ReadAsAsync<RoleReadDto>();

            var roleOnDb = await Helpers.Role.GetRoleAsync(r => r.Name == roleToCreate.Name);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(result);

            Assert.Equal(roleToCreate.Name, result.Name);

            Assert.NotNull(roleOnDb);
            Assert.Equal(roleToCreate.Name, roleOnDb.Name);
        }


        [Fact]
        public async Task DeleteRole_SuccessfullyDeleteRole()
        {
            // Arrange
            var roleCreated = await Helpers.Role.CreateNewRoleAsync();
            var roleOnDbBeforeAct = await Helpers.Role.GetRoleAsync(r => r.Id == roleCreated.Id);

            var route = ApiRoutes.Roles.DeleteRole;
            var uri = route.Replace("{id}", roleCreated.Id.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.DeleteAsync(uri);

            // Assert
            var roleOnDb = await Helpers.Role.GetRoleAsync(r => r.Id == roleCreated.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.NotNull(roleOnDbBeforeAct);
            Assert.Null(roleOnDb);
        }

        public async Task InitializeAsync()
        {
            await Context.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
        }
    }
}