using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Saskaita.API.Application.Services;
using Saskaita.Controllers;
using Saskaita.Domain.Models;
using Saskaita.Repository;
using System.Text;

namespace Saskaita.Test
{
    public class InvoiceControllerTest
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock = new();
        private readonly Mock<ICountryRepository> _countryRepositoryMock = new();

        [Fact]
        public void CreateInvoice_ValidRequest_InvoiceCreated()
        {
            //Arrange
            var pdf = new ChromePdfRenderer();
            var pdfString = "<h1>This is a heading</h1>";
            _invoiceServiceMock.Setup(x => x.CreateInvoice(It.IsAny<InvoiceRequest>())).Returns(pdf.RenderHtmlAsPdf(pdfString));
            _countryRepositoryMock.Setup(x => x.GetSupportedCountryList()).Returns(new List<string> { "LT" });
            var service = new InvoiceController(_invoiceServiceMock.Object, _countryRepositoryMock.Object);
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

            //Act
            var response = (FileContentResult)service.Post(requestBody: requestBody);
            string textContents = new UTF8Encoding().GetString(response.FileContents);

            //Assert
            Assert.Contains("PDF", textContents);

        }

        [Fact]
        public void CreateInvoice_BadRequest_BadRequestResponseReturned()
        {
            //Arrange
            var pdf = new ChromePdfRenderer();
            var pdfString = "<h1>This is a heading</h1>";
            _invoiceServiceMock.Setup(x => x.CreateInvoice(It.IsAny<InvoiceRequest>())).Returns(pdf.RenderHtmlAsPdf(pdfString));
            _countryRepositoryMock.Setup(x => x.GetSupportedCountryList()).Returns(new List<string> { "LT" });
            var service = new InvoiceController(_invoiceServiceMock.Object, _countryRepositoryMock.Object);

            //Act
            var response = service.Post(requestBody: new InvoiceRequest());

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}