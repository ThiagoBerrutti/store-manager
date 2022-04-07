using StoreAPI.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Store.API.UnitTests
{
    public class AgeCalculatorTests
    {

        public AgeCalculatorTests()
        {
            
        }

        public DateTime GenerateDoBFromAge(int age) => DateTime.UtcNow.AddYears(-age);


        //public List<DateTime> GenerateDoBs(int count)
        //{
        //    var result = new List<DateTime>();
        //    var random = new Random();

        //    for (int i = 0; i < count; i++)
        //    {
        //        var age = 18 + random.Next(0, 52);

        //        result.Add(GenerateDoBFromAge(age));
        //    }

        //    return result;
        //}

        [Theory]
        [InlineData(24)]
        [InlineData(19)]
        [InlineData(32)]
        [InlineData(57)]
        public void VerifyAges(int age)
        {
            // Arrange
            var dob = GenerateDoBFromAge(age);

            var dobOnlyDate = dob.Date;
            var dobLastTickOfSameDay = dobOnlyDate.AddDays(1).AddTicks(-1);
            var dobFirstTickNextDay = dobOnlyDate.AddDays(1);
            

            // Act
            var dobResult = AgeCalculator.Calculate(dob);
            var onlyDateResult = AgeCalculator.Calculate(dobOnlyDate);
            var lastTickSameDayResult = AgeCalculator.Calculate(dobLastTickOfSameDay);
            var firstTickNextDayResult = AgeCalculator.Calculate(dobFirstTickNextDay);

            // Assert 
            Assert.Equal(age, dobResult);
            Assert.Equal(age, onlyDateResult);
            Assert.Equal(age, lastTickSameDayResult);
            Assert.Equal(age - 1, firstTickNextDayResult);
        }
    }
}
