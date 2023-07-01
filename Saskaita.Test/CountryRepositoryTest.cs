using IronPdf;
using Saskaita.Repository;
using System.Diagnostics.Metrics;

namespace Saskaita.Test
{
    public class CountryRepositoryTest
    {
        [Theory]
        [InlineData("LT", true)]
        [InlineData("FR", true)]
        [InlineData("CA", false)]
        [InlineData("JP", false)]
        public void IsCountryInEU_CheckAllCountries_ReturnsCorrectInfo(string country, bool expectedResult)
        {
            //Arrange
            var service = new CountryRepository();

            //Act
            var result = service.IsCountryInEU(country);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("LT", 21)]
        [InlineData("FR", 20)]
        [InlineData("CA", 5)]
        [InlineData("JP", 10)]
        public void GetCountryVatRate_CheckAllCountries_ReturnsCorrectInfo(string country, int expectedVatRate)
        {
            //Arrange
            var service = new CountryRepository();

            //Act
            var result = service.GetCountryVatRate(country);

            //Assert
            Assert.Equal(expectedVatRate, result);
        }

        [Fact]
        public void GetSupportedCountryList_GetList_ReturnsAllCountries()
        {
            //Arrange
            var service = new CountryRepository();

            //Act
            var result = service.GetSupportedCountryList();

            //Assert
            Assert.Equal("LT", result[0]);
            Assert.Equal("CA", result[1]);
            Assert.Equal("FR", result[2]);
            Assert.Equal("JP", result[3]);
        }
    }
}