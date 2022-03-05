
using RandomUser.Models;
using StoreAPI.TestUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserRandom
{
    public class RandomUser
    {
        public class NameRandomizer
        {
            private Random Random = new Random();
            private int MaxLastNameCount { get; set; }


            private static string RandomUserNameNumber()
            {
                var random = new Random();
                var number = random.Next(int.MaxValue).ToString();

                return number;
            }


            public string RandomName(int minLength, int maxLength)
            {
                int minCharRange = 'a';
                int maxCharRange = 'z';

                bool insertingVowel = Random.Next() % 2 == 0;
                char randomLetter;

                var nameLength = Random.Next(minLength, maxLength);
                var name = "";

                while (name.Length < nameLength)
                {
                    if (insertingVowel)
                    {
                        randomLetter = Enum.GetName(typeof(Vowels), Random.Next(1, 6))[0];
                    }
                    else
                    {
                        do
                        {
                            randomLetter = (char)Random.Next(minCharRange, maxCharRange);

                        } while (IsVowel(randomLetter) || !IsValidLetter(randomLetter));
                    }

                    insertingVowel = !insertingVowel;

                    name += randomLetter;
                }

                var capitalizedName = char.ToUpper(name[0]) + name[1..];
                return capitalizedName;
            }


            public string RandomMultipleLastNames(int minLength, int maxLength, int maxTotalLength, int minLastNameCount = 0, int maxLastNameCount = 0)
            {
                var maxLastNameCountPossibleOnThis = (int)Math.Floor(1.0 * maxTotalLength / minLength);
                var minLastNameCountPossibleOnThis = (int)Math.Floor(1.0 * maxTotalLength / maxLength);

                var theLesserMaxCount = maxLastNameCount <= 0 ? maxLastNameCountPossibleOnThis : Math.Min(maxLastNameCount, maxLastNameCountPossibleOnThis);
                //var theLesserMinCount = minLastNameCount <= 0 ? minLastNameCountPossibleOnThis : Math.Min(minLastNameCount, minLastNameCountPossibleOnThis);

                var randomCount = Random.Next(1, theLesserMaxCount);

                List<string> lastNames = new List<string>();

                var totalLength = 0;
                for (int i = 0; i < randomCount && totalLength < maxTotalLength; i++)
                {
                    var lastName = RandomName(minLength, maxLength);
                    totalLength += lastName.Length;
                    if (totalLength <= maxTotalLength)
                    {
                        lastNames.Add(lastName);
                    }
                }

                var result = string.Join(' ', lastNames);

                return result;
            }


            private static bool IsVowel(char letter)
            {
                var lowerLetter = char.ToLower(letter);
                var vowels = Enum.GetNames(typeof(Vowels))
                                        .Select(s => char.ToLower(s[0]));

                return vowels.Contains(letter);
            }


            private static bool IsValidLetter(char letter)
            {
                var lowerLetter = char.ToLower(letter);
                var invalidLetters = Enum.GetNames(typeof(InvalidLetters))
                                        .Select(s => char.ToLower(s[0]));

                return !invalidLetters.Contains(letter);
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


            private enum InvalidLetters
            {
                q,
                w,
                k
            }


            public string RandomFullName(int minNameLength, int maxNameLength, int maxTotalNameLength, int maxLastNameCount)
            {
                var firstName = RandomName(minNameLength, maxNameLength);
                var lastNameMaxLength = maxTotalNameLength - firstName.Length - 1;
                var lastName = RandomMultipleLastNames(minNameLength, maxNameLength, lastNameMaxLength, maxLastNameCount);
                var fullName = firstName + " " + lastName;

                return fullName;
            }
        }




        public static void PrintNames(int minNameLength = 3, int maxNameLength = 10, int maxTotalNameLength = 30, int maxLastNameCount = 0)
        {
            var nameRandomizer = new NameRandomizer();
            char key;
            var i = 1;
            do
            {
                var fullName = nameRandomizer.RandomFullName(minNameLength, maxNameLength, maxTotalNameLength, maxLastNameCount);

                var allNames = new List<string>(fullName.Split(' '));
                Console.WriteLine($"#{i}: {fullName}");


                for (int n = 0; n < allNames.Count; n++)
                {
                    Console.WriteLine($"   - {allNames[n]}: [Length: {allNames[n].Length}]");

                }

                var totalLengthText = $"   Total length: {fullName.Length} [";
                allNames.ForEach(n =>
                {
                    totalLengthText += n.Length;

                    if (allNames.IndexOf(n) != allNames.Count - 1)
                    {
                        totalLengthText += " + ";
                    }
                    else
                    {
                        totalLengthText += $" = {allNames.Select(n => n.Length).Sum()}";
                    }
                });

                totalLengthText += $" (+ {fullName.Where(c => c == ' ').Count()} spaces)]\n";

                Console.WriteLine(totalLengthText);

                i++;

                key = Console.ReadKey(true).KeyChar;

            } while (key != 27);
        }

        public static async Task<Response> GetUser()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5002/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("https://randomuser.me/api/?nat=br&format=pretty");

            var y = await response.Content.ReadAsStringAsync();
            var x = JsonSerializer.Deserialize<Response>(y);

            var formatters = new List<MediaTypeFormatter>()
            {
                new JsonMediaTypeFormatter()
            };
            var json = await response.Content.ReadAsAsync<Response>(formatters);

            return json;
        }
    }
}
