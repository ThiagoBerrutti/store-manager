using StoreAPI.Dtos;
using StoreAPI.Infra;
using System;

namespace StoreAPI.Helpers
{
    public static class TestAccountUserRegisterFactory
    {
        public static UserRegisterDto Produce(int maxDigits)
        {
            var randomUserName = RandomUserNameNumber(maxDigits);
            var user = new UserRegisterDto
            {
                UserName = "user" + randomUserName,
                Password = "test",
                FirstName = "TestUser" + randomUserName,
                LastName = "LastName",
                DateOfBirth = RandomDateOfBirth(50),
            };

            return user;
        }


        public static DateTime RandomDateOfBirth(int maxAge)
        {
            if (maxAge < AppConstants.Validations.User.MinimumAge)
            {
                maxAge = AppConstants.Validations.User.MinimumAge;
            }

            var random = new Random();

            DateTime max = DateTime.UtcNow.AddYears(-AppConstants.Validations.User.MinimumAge-1).AddDays(1);
            DateTime min = DateTime.UtcNow.AddYears(-maxAge);
            var range = max - min;
            var date = min.AddDays(random.Next(range.Days));

            return date;
        }


        public static string RandomUserNameNumber(int maxDigits)
        {
            if (maxDigits < 1) return "";

            var maxNumber = Math.Pow(10, maxDigits) - 1;
            maxNumber = Math.Min(int.MaxValue, maxNumber);

            var random = new Random();
            
            var number = random.Next((int)maxNumber).ToString();

            return number;
        }
    }
}