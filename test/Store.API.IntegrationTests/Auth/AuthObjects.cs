using StoreAPI.Dtos;
using StoreAPI.Infra;
using System;

namespace Store.API.IntegrationTests.Auth
{
    public class AuthObjects
    {
        public static class UserLogins
        {
            public static UserLoginDto Admin = new UserLoginDto
            {
                UserName = AppConstants.Users.Admin.UserName,
                Password = AppConstants.Users.Admin.Password
            };

            public static UserLoginDto Manager = new UserLoginDto
            {
                UserName = AppConstants.Users.Manager.UserName,
                Password = AppConstants.Users.Manager.Password
            };

            public static UserLoginDto Stock = new UserLoginDto
            {
                UserName = AppConstants.Users.Stock.UserName,
                Password = AppConstants.Users.Stock.Password
            };

            public static UserLoginDto Seller = new UserLoginDto
            {
                UserName = AppConstants.Users.Seller.UserName,
                Password = AppConstants.Users.Seller.Password
            };

            public static UserLoginDto Public = new UserLoginDto
            {
                UserName = AppConstants.Users.Public.UserName,
                Password = AppConstants.Users.Public.Password
            };
        }

        public static class Factories
        {
            public static UserRegisterDto UserRegisterDto()
            {
                var random = new Random().Next(1000, 9999);
                return new UserRegisterDto
                {
                    DateOfBirth = new DateTime(2000, 12, 25),
                    FirstName = "First" + random,
                    LastName = "Last" + random,
                    Password = "12345",
                    UserName = "testuser" + random
                };
            }
        }
    }
}