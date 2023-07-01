using Saskaita.Domain.Models;

namespace Saskaita.API.Application.Services
{
    public interface IInvoiceService
    {
        PdfDocument CreateInvoice(InvoiceRequest request);
    }
}
