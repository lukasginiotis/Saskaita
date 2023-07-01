using Moq;
using Saskaita.API.Application.Services;
using Saskaita.Domain.Models;
using Saskaita.Repository;

namespace Saskaita.Test
{
    public class InvoiceRequestValidatorTest
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock = new();

        [Fact]
        public void InvoiceRequestValidator_ValidData_Passes()
        {
            //Arrange
            var requestBody = new InvoiceRequest
            {
                BuyerCompanyName = "Company1",
                BuyerAddress = "Neverland",
                BuyerVatCode = "Vat1",
                BuyerCompanyCode = "Comp1",
                BuyerCountry = "LT",
                SellerCompanyName = "Company2",
                SellerAddress = "Somewhere",
                SellerVatCode = "Vat2",
                SellerCompanyCode = "Comp2",
                SellerCountry = "LT",
                ServiceName = "MoneyPrinting",
                SellerBank = "MyBank",
                SellerBankAccountNumber = "LT1",
                Units = 1,
                UnitPrice = 20
            };
            var validator = new InvoiceRequestValidator(_countryRepositoryMock.Object);
            _countryRepositoryMock.Setup(x => x.GetSupportedCountryList()).Returns(new List<string> { "LT" });

            //Act
            var validationResult = validator.Validate(requestBody);

            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Theory]
        [InlineData("Company1", "LT", 0)]
        [InlineData("Company1", null, 1)]
        [InlineData("Company1", "Country", 1)]
        [InlineData("", "LT", 1)]
        public void InvoiceRequestValidator_InvalidData_Fails(string buyerCompanyName, string buyerCountry, int units)
        {
            //Arrange
            var requestBody = new InvoiceRequest
            {
                BuyerCompanyName = buyerCompanyName,
                BuyerAddress = "Neverland",
                BuyerVatCode = "Vat1",
                BuyerCompanyCode = "Comp1",
                BuyerCountry = buyerCountry,
                SellerCompanyName = "Company2",
                SellerAddress = "Somewhere",
                SellerVatCode = "Vat2",
                SellerCompanyCode = "Comp2",
                SellerCountry = "LT",
                ServiceName = "MoneyPrinting",
                SellerBank = "MyBank",
                SellerBankAccountNumber = "LT1",
                Units = units,
                UnitPrice = 20
            };
            var validator = new InvoiceRequestValidator(_countryRepositoryMock.Object);
            _countryRepositoryMock.Setup(x => x.GetSupportedCountryList()).Returns(new List<string> { "LT" });

            //Act
            var validationResult = validator.Validate(requestBody);

            //Assert
            Assert.False(validationResult.IsValid);
        }
    }
}