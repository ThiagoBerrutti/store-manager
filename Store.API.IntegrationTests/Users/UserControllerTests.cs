using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Store.API.IntegrationTests.Roles;
using StoreAPI;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Store.API.IntegrationTests.Users
{
    public class UserControllerTests : TestBase, IAsyncLifetime
    {
        public UserControllerTests(TestWebApplicationFactory<Startup> factory) : base(factory)
        {
        }


        [Fact]
        public async Task GetAllUsersPaginated_SuccessfullyReturnAllUsers()
        {
            // Arrange
            const int NEW_USER_COUNT = 5;
            var usersCreated = await Helpers.User.CreateNewUsersAsync(NEW_USER_COUNT);

            var allUsersBeforeAct = await Helpers.User.GetUsersAsync();

            var usersBeforeActIds = allUsersBeforeAct.Select(u => u.Id);
            var usersBeforeActUsernames = allUsersBeforeAct.Select(u => u.UserName);

            var uri = ApiRoutes.Users.GetAllUsersPaginated;
            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var responseUsers = await response.Content.ReadAsAsync<List<UserReadDto>>();

            var result = responseUsers.All(u =>
                    usersBeforeActIds.Contains(u.Id) && 
                    usersBeforeActUsernames.Contains(u.UserName));

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(allUsersBeforeAct.Count, responseUsers.Count);
            Assert.True(result);
        }


        [Fact]
        public async Task GetUserById_SuccessfullyReturnsUser()
        {
            // Arrange
            var userCreated = await Helpers.User.CreateNewUserAsync();
            var id = userCreated.Id;

            var route = ApiRoutes.Users.GetUserById;
            var uri = route.Replace("{id}", id.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<UserDetailedReadDto>();

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(userCreated.UserName, result.UserName);
        }


        [Fact]
        public async Task GetCurrentUser_SuccessfullyReturnsUserAuthenticated()
        {
            // Arrange
            var userCreated = await Helpers.User.CreateNewUserAsync();
            
            var uri = ApiRoutes.Users.GetCurrentUser;

            await Helpers.AuthenticateAsync(userCreated.UserName, UserObjects.Password);

            // Act
            var response = await Client.GetAsync(uri);
            var currentUser = await response.Content.ReadAsAsync<UserDetailedReadDto>();

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(currentUser.UserName, userCreated.UserName);
            Assert.Equal(currentUser.Id, userCreated.Id);
        }


        [Fact]
        public async Task GetRolesFromUser_ReturnRolesSuccessfully()
        {
            // Arrange
            var rolesToAdd = await Helpers.Role.CreateNewRolesAsync(5);
            var roleIds = rolesToAdd.Select(r => r.Id);

            var user = await Helpers.User.CreateNewUserAsync();
            await Helpers.User.AddRolesToUser(user.Id, roleIds);

            var route = ApiRoutes.Users.GetRolesFromUser;
            var uri = route.Replace("{id}", user.Id.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsAsync<List<RoleReadDto>>();

            var resultIds = result.Select(r => r.Id);

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(resultIds, roleIds);
        }


        [Fact]
        public async Task UpdateUser_SuccessfullyUpdatesUser()
        {
            // Arrange
            var userCreated = await Helpers.User.CreateNewUserAsync();
            var userId = userCreated.Id;

            const int YEARS_ADDED = 30;
            var userUpdate = new UserUpdateDto
            {
                DateOfBirth = userCreated.DateOfBirth.AddYears(-YEARS_ADDED),
                FirstName = "Updated" + UserTestHelpers.NumbersInString(userCreated.UserName),
                LastName = "Updated"
            };
              
            var route = ApiRoutes.Users.UpdateUser;
            var uri = route.Replace("{id}", userId.ToString());

            await Helpers.AuthenticateAsAdminAsync();

            // Act
            var response = await Client.PutAsJsonAsync(uri, userUpdate);
            var result = await response.Content.ReadAsAsync<UserReadDto>();

            // Assert 
            var userUpdateFullName = UserTestHelpers.CreateFullName(userUpdate.FirstName, userUpdate.LastName);
            var userCreatedFullName = UserTestHelpers.CreateFullName(userCreated.FirstName, userCreated.LastName);

            var userOnDbAfterAct = await Helpers.User.GetUserAsync(u => u.Id == userCreated.Id);
            var userOnDbAfterActFullName = UserTestHelpers.CreateFullName(userOnDbAfterAct.FirstName, userOnDbAfterAct.LastName);

            var userUpdateAge = AgeCalculator.Calculate(userUpdate.DateOfBirth);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userCreated.Id, result.Id);

                // asserts data was updated
            Assert.NotEqual(userOnDbAfterActFullName, userCreatedFullName);
            Assert.NotEqual(userCreated.DateOfBirth, userOnDbAfterAct.DateOfBirth);

                // asserts data on db is the same as update
            Assert.Equal(userOnDbAfterActFullName, userUpdateFullName);
            Assert.Equal(userOnDbAfterAct.DateOfBirth, userUpdate.DateOfBirth);

                // asserts data result is the same as update
            Assert.Equal(result.FullName, userUpdateFullName);
            Assert.Equal(result.Age, userUpdateAge);

        }

        [Fact]
        public async Task UpdateCurrentUser_SuccessfullyUpdatesCurrentUser()
        {
            // Arrange
            var userCreated = await Helpers.User.CreateNewUserAsync();
            var userId = userCreated.Id;

            const int YEARS_ADDED = 30;
            var userUpdate = new UserUpdateDto
            {
                DateOfBirth = userCreated.DateOfBirth.AddYears(-YEARS_ADDED),
                FirstName = "Updated" + UserTestHelpers.NumbersInString(userCreated.UserName),
                LastName = "Updated"
            };

            var uri = ApiRoutes.Users.UpdateCurrentUser;

            await Helpers.AuthenticateAsync(userCreated.UserName, UserObjects.Password);

            // Act
            var response = await Client.PutAsJsonAsync(uri, userUpdate);
            var result = await response.Content.ReadAsAsync<UserReadDto>();

            // Assert 
            var userUpdateFullName = UserTestHelpers.CreateFullName(userUpdate.FirstName, userUpdate.LastName);
            var userCreatedFullName = UserTestHelpers.CreateFullName(userCreated.FirstName, userCreated.LastName);

            var userOnDbAfterAct = await Helpers.User.GetUserAsync(u => u.Id == userCreated.Id);
            var userOnDbAfterActFullName = UserTestHelpers.CreateFullName(userOnDbAfterAct.FirstName, userOnDbAfterAct.LastName);

            var userUpdateAge = AgeCalculator.Calculate(userUpdate.DateOfBirth);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userCreated.Id, result.Id);

            // asserts data was updated
            Assert.NotEqual(userOnDbAfterActFullName, userCreatedFullName);
            Assert.NotEqual(userCreated.DateOfBirth, userOnDbAfterAct.DateOfBirth);

            // asserts data on db is the same as update
            Assert.Equal(userOnDbAfterActFullName, userUpdateFullName);
            Assert.Equal(userOnDbAfterAct.DateOfBirth, userUpdate.DateOfBirth);

            // asserts data result is the same as update
            Assert.Equal(result.FullName, userUpdateFullName);
            Assert.Equal(result.Age, userUpdateAge);
        }


        [Fact]
        public async Task ChangeCurrentUserPassword_SuccessfullyChangesCurrentUserPassword()
        {
            // Arrange
            var userCreated = await Helpers.User.CreateNewUserAsync();

            const string OLD_PASSWORD = UserObjects.Password;
            const string NEW_PASSWORD = "newPassword123";

            var changePasswords = new ChangePasswordDto
            {
                CurrentPassword = UserObjects.Password,
                NewPassword = NEW_PASSWORD
            };

            var uri = ApiRoutes.Users.ChangeCurrentUserPassword;

            var userLogin = new UserLoginDto 
            { 
                Password = OLD_PASSWORD, 
                UserName = userCreated.UserName 
            };

            await Helpers.AuthenticateAsync(userLogin);

            // Act            
            var response = await Client.PutAsJsonAsync(uri, changePasswords);
            var currentUser = await Helpers.User.GetUserAsync(u => u.Id == userCreated.Id);

            var hasher = new PasswordHasher<User>();
            var oldPasswordVerificationResult = hasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, OLD_PASSWORD);
            var newPasswordVerificationResult = hasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, NEW_PASSWORD);
          
            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(PasswordVerificationResult.Failed, oldPasswordVerificationResult);
            Assert.Equal(PasswordVerificationResult.Success, newPasswordVerificationResult);
        }


        [Fact]
        public async Task ResetPassword_WithEmptyPasswordParameter_ResetsPasswordToItsUsername()
        {
            // Arrange
            var userCreated = await Helpers.User.CreateNewUserAsync();

            var id = userCreated.Id;

            var route = ApiRoutes.Users.ResetPassword;
            var uri = route.Replace("{id}", id.ToString());

            var oldPassword = UserObjects.Password;
            var newPassword = userCreated.UserName;

            var hasher = new PasswordHasher<User>();

            // Act
            var response = await Client.PutAsJsonAsync(uri, "");

            var user = await Helpers.User.GetUserAsync(u => u.Id == id);
            var oldPasswordVerifyResult = hasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
            var newPasswordVerifyResult = hasher.VerifyHashedPassword(user, user.PasswordHash, newPassword);
            
            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(PasswordVerificationResult.Failed, oldPasswordVerifyResult);
            Assert.Equal(PasswordVerificationResult.Success, newPasswordVerifyResult);
            
        }


        [Fact]
        public async Task AddUserToRole_SuccessfullyAddsRole()
        {
            // Arrange

            // Act

            // Assert 
        }


        [Fact]
        public async Task RemoveFromRole_SuccessfullyRemovesRole()
        {
            // Arrange

            // Act

            // Assert 
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