using IronPdf;
using Moq;
using Saskaita.API.Application.Services;
using Saskaita.Domain.Models;

namespace Saskaita.Test
{
    public class InvoiceServiceTest
    {
        private readonly Mock<IPriceCalculationService> _priceCalculationServiceMock = new();

        [Fact]
        public void CreateInvoice_CorrectInput_DocumentReturned()
        {
            //Arrange
            var request = new InvoiceRequest();
            _priceCalculationServiceMock.Setup(x => x.CalculatePrice(It.IsAny<InvoiceRequest>())).Returns((20, 20, 24));
            var service = new InvoiceService(_priceCalculationServiceMock.Object);

            //Act
            var result = service.CreateInvoice(request);

            //Assert
            Assert.IsType<PdfDocument>(result);
        }
    }
}