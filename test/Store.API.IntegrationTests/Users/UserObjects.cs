using Microsoft.AspNetCore.Identity;
using StoreAPI.Identity;
using StoreAPI.Infra;
using System;

namespace Store.API.IntegrationTests.Users
{
    public static class UserObjects
    {
        public const string Password = "test";
        public const string BaseFirstName = "TestUser";
        public const string BaseLastName = "LastName";
        public const string UpdatedName = "Updated";

        public static class Factory
        {
            public static User GenerateUser()
            {
                var hasher = new PasswordHasher<User>();
                var random = new Random().Next(1000, 9999);

                var user = new User
                {
                    SecurityStamp = new Guid().ToString(),
                    FirstName = BaseFirstName + random,
                    LastName = BaseLastName,
                    DateOfBirth = RandomDateOfBirth(50),
                };

                user.PasswordHash = hasher.HashPassword(user, Password);
                user.UserName = user.FirstName;
                user.NormalizedUserName = user.UserName.ToUpper();

                return user;
            }
        }


        public static DateTime RandomDateOfBirth(int maxAge = 80)
        {
            if (maxAge < AppConstants.Validations.User.MinimumAge)
            {
                maxAge = AppConstants.Validations.User.MinimumAge;
            }

            var random = new Random();

            DateTime max = DateTime.UtcNow.AddYears(-AppConstants.Validations.User.MinimumAge - 1).AddDays(1);
            DateTime min = DateTime.UtcNow.AddYears(-maxAge);
            var range = max - min;
            var date = min.AddDays(random.Next(range.Days));

            return date;
        }
    }
}