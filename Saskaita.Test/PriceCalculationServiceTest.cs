using Moq;
using Saskaita.API.Application.Services;
using Saskaita.Domain.Models;
using Saskaita.Repository;

namespace Saskaita.Test
{
    public class PriceCalculationServiceTest
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock = new();

        [Fact]
        public void CalculatePrice_SameCountry_VatApplied()
        {
            //Arrange
            var request = new InvoiceRequest
            {
                BuyerCountry = "JP",
                SellerCountry = "JP",
                Units = 2,
                UnitPrice = 15
            };
            _countryRepositoryMock.Setup(x => x.IsCountryInEU("JP")).Returns(false);
            _countryRepositoryMock.Setup(x => x.GetCountryVatRate("JP")).Returns(20);
            var service = new PriceCalculationService(_countryRepositoryMock.Object);

            //Act
            (decimal priceBeforeVat, int vatApplied, decimal priceAfterVat) = service.CalculatePrice(request);

            //Assert
            Assert.Equal(30, priceBeforeVat);
            Assert.Equal(20, vatApplied);
            Assert.Equal(36, priceAfterVat);
        }

        [Fact]
        public void CalculatePrice_BothEU_VatApplied()
        {
            //Arrange
            var request = new InvoiceRequest
            {
                BuyerCountry = "LT",
                SellerCountry = "FR",
                Units = 2,
                UnitPrice = 15
            };
            _countryRepositoryMock.Setup(x => x.IsCountryInEU("LT")).Returns(true);
            _countryRepositoryMock.Setup(x => x.GetCountryVatRate("LT")).Returns(21);
            var service = new PriceCalculationService(_countryRepositoryMock.Object);

            //Act
            (decimal priceBeforeVat, int vatApplied, decimal priceAfterVat) = service.CalculatePrice(request);

            //Assert
            Assert.Equal(30, priceBeforeVat);
            Assert.Equal(21, vatApplied);
            Assert.Equal(36.3m, priceAfterVat);
        }

        [Fact]
        public void CalculatePrice_BuyerNotEU_VatNotApplied()
        {
            //Arrange
            var request = new InvoiceRequest
            {
                BuyerCountry = "CA",
                SellerCountry = "JP",
                Units = 2,
                UnitPrice = 15
            };
            var service = new PriceCalculationService(_countryRepositoryMock.Object);

            //Act
            (decimal priceBeforeVat, int vatApplied, decimal priceAfterVat) = service.CalculatePrice(request);

            //Assert
            Assert.Equal(30, priceBeforeVat);
            Assert.Equal(0, vatApplied);
            Assert.Equal(30, priceAfterVat);
        }
    }
}