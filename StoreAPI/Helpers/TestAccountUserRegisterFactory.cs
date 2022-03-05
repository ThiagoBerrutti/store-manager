using StoreAPI.Dtos;
using StoreAPI.Infra;
using System;

namespace StoreAPI.Helpers
{
    public static class TestAccountUserRegisterFactory
    {
        public static UserRegisterDto Produce()
        {
            var randomUserName = RandomUserNameNumber();
            var user = new UserRegisterDto
            {

                UserName = "user" + randomUserName,
                Password = "test",
                FirstName = "TestUser" + randomUserName,
                LastName = "LastName",
                DateOfBirth = RandomDateOfBirth(),
            };

            return user;
        }


        public static DateTime RandomDateOfBirth()
        {
            var random = new Random();

            int range = 365 * 50;
            DateTime max = DateTime.UtcNow.AddYears(-AppConstants.Validations.User.MinimumAge);
            DateTime min = max.AddDays(-range);

            var date = min.AddDays(random.Next(range));

            return date;
        }



        public static string RandomUserNameNumber()
        {
            var random = new Random();
            var number = random.Next(int.MaxValue).ToString();

            return number;
        }

        public static string RandomLastName()
        {
            var random = new Random();

            var minLastNameCount = 1;
            var maxLastNameCount = 4;

            var minLastNameLength = 3;
            var maxLastNameLength = 7;

            var lastNamesCount = random.Next(minLastNameCount, maxLastNameCount);
            var lastNamesLength = random.Next(7, 25);


            var lastName = RandomMultipleLastNames(lastNamesCount, minLastNameLength, maxLastNameLength, lastNamesLength);

            return lastName;
        }

        private static string RandomName(int minLength, int maxLength)
        {
            var random = new Random();

            int minCharRange = 'a';
            int maxCharRange = 'z';

            bool insertingVowel = random.Next() % 2 == 0;
            char randomLetter;

            var name = "";

            while (name.Length < maxLength)
            {
                if (insertingVowel)
                {
                    randomLetter = Enum.GetName(typeof(Vowels), random.Next(1, 6))[0];
                }
                else
                {
                    do
                    {
                        randomLetter = (char)random.Next(minCharRange, maxCharRange);

                    } while (IsVowel(randomLetter));
                }

                name += randomLetter;
            }

            var capitalizedName = char.ToUpper(name[0]) + name[1..];
            return capitalizedName;
        }


        private static string RandomMultipleLastNames(int count, int minLength, int maxLength, int maxTotalLength)
        {
            string[] lastNames = { };
            var totalLength = 0;

            for (int i = 0; i < count && totalLength < maxTotalLength ; i++)
            {
                var lastName = RandomName(minLength, maxLength);
                totalLength += lastName.Length;
                if (totalLength <= maxTotalLength)
                {
                    lastNames[i] = lastName;
                }
            }

            var result = string.Join(' ', lastNames);

            return result;
        }

        private static bool IsVowel(char letter)
        {
            var lowerLetter = char.ToLower(letter);

            return lowerLetter == 'a' || lowerLetter == 'e' ||
                   lowerLetter == 'i' || lowerLetter == 'o' ||
                   lowerLetter == 'u' || lowerLetter == 'y';
        }

        private enum Vowels : int
        {
            a = 1,
            e = 2,
            i = 3,
            o = 4,
            u = 5,
            y = 6
        }
    }
}