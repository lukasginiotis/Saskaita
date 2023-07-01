using Saskaita.Domain.Models;
using Saskaita.Repository;

namespace Saskaita.API.Application.Services
{
    public class PriceCalculationService : IPriceCalculationService
    {
        public ICountryRepository _countryRepository;
        public PriceCalculationService(ICountryRepository countryRepository) 
        {
            _countryRepository = countryRepository;
        }
        public (decimal, int, decimal) CalculatePrice(InvoiceRequest request)
        {
            int vat = (request.BuyerCountry == request.SellerCountry || _countryRepository.IsCountryInEU(request.BuyerCountry))
                ? _countryRepository.GetCountryVatRate(request.BuyerCountry)
                : 0;
            decimal pricePreVat = (request.Units * request.UnitPrice);
            decimal price = pricePreVat * (1 + ((decimal)vat / 100));
            return (Math.Round(pricePreVat,2, MidpointRounding.AwayFromZero), vat, Math.Round(price, 2, MidpointRounding.AwayFromZero));
        }
    }
}
