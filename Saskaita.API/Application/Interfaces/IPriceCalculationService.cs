using Saskaita.Domain.Models;

namespace Saskaita.API.Application.Services
{
    public interface IPriceCalculationService
    {
        (decimal, int, decimal) CalculatePrice(InvoiceRequest request);
    }
}
