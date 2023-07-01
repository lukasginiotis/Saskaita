using Saskaita.Domain.Models;

namespace Saskaita.API.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        public IPriceCalculationService _priceCalculationService;
        public InvoiceService(IPriceCalculationService priceCalculationService) 
        {
            _priceCalculationService = priceCalculationService;
        }
        public PdfDocument CreateInvoice(InvoiceRequest request)
        {
            (decimal totalPricePreVat, int vatApplied, decimal totalPrice) = _priceCalculationService.CalculatePrice(request);
            return RenderDocument(request, totalPricePreVat, vatApplied, totalPrice);
        }

        private static PdfDocument RenderDocument(InvoiceRequest request, decimal totalPricePreVat, int vatApplied, decimal totalPrice)
        {
            var pdf = new ChromePdfRenderer();
            var pdfString = "  <head>\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n<style>\r\n* {\r\n  box-sizing: border-box;\r\n}\r\n\r\n.column {\r\n  float: left;\r\n  width: 50%;\r\n  padding: 10px;\r\n}\r\n\r\n.columnWide {\r\n  float: left;\r\n  width: 60%;\r\n  padding: 10px;\r\n}\r\n\r\n.columnNarrow {\r\n  float: left;\r\n  width: 10%;\r\n  padding: 10px;\r\n}\r\n\r\n.row:after {\r\n  content: \"\";\r\n  display: table;\r\n  clear: both;\r\n  text-align: center;\r\n}\r\n</style>\r\n</head>" +
                $"<body><div><center>Data: {DateTime.Today.ToShortDateString()}</center></div>" +
                $"<div class=\"column\">\r\n    <p><b>SASKAITA - FAKTURA</b></p>\r\n   <p><b>Pardavejas: </b></p>\r\n\t<p>UAB \"{request.SellerCompanyName}\"</p>\r\n\t<p>Im. kodas: {request.SellerCompanyCode}</p>\r\n\t<p>PVM kodas: {request.SellerVatCode}</p>\r\n\t<p>Adresas: {request.SellerAddress}</p>\r\n\t<p>{request.SellerBankAccountNumber}</p>\r\n\t<p>Bankas {request.SellerBank}</p>\r\n  </div>" +
                $"<div class=\"column\">\r\n    <p><b>Serijos nr. 123456</b></p>\r\n    <p><b>Pirkejas: </b></p>\r\n\t<p>UAB \"{request.BuyerCompanyName}\"</p>\r\n\t<p>Im. kodas: {request.BuyerCompanyCode}</p>\r\n\t<p>PVM kodas: {request.BuyerVatCode}</p>\r\n\t<p>Adresas: {request.BuyerAddress}</p>\r\n  </div></div>" +
                $"<div class=\"row\">\r\n  <div class=\"columnWide\">\r\n    <p><b>Prekes pavadinimas</b</p>\r\n\t<p>{request.ServiceName}</p>\r\n  </div>\r\n  <div class=\"columnNarrow\">\r\n    <p><b>Kiekis</b></p>\r\n\t<p>{request.Units}</p>\r\n\t<p><b>PVM</b></p>\r\n  </div>\r\n  <div class=\"columnNarrow\">\r\n    <p><b>Kaina</b></p>\r\n\t<p>{request.UnitPrice}</p>\r\n\t<p>{vatApplied}%</p>\r\n  </div>\r\n    <div class=\"columnNarrow\">\r\n    <p><b>Suma</b></p>\r\n\t<p>{totalPricePreVat}</p>\r\n\t<p>{totalPrice - totalPricePreVat}</p>\r\n\t<p><b>{totalPrice}</b></p>\r\n  </div>\r\n    <div class=\"columnNarrow\">\r\n    <p>_</p>\r\n\t<p>EUR</p>\r\n\t<p>EUR</p>\r\n\t<p>EUR</p>\r\n  </div>\r\n</div>" +
                "<div class=\"row\">\r\n  <div class=\"column\">\r\n    <p>Pardavejo parasas:</p>\r\n  </div>\r\n  <div class=\"column\">\r\n    <p>Pirkejo parasas:</p>\r\n  </div>\r\n</div>";
            return pdf.RenderHtmlAsPdf(pdfString);
        }
    }
}
