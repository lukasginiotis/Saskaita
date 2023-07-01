using FluentValidation;
using Saskaita.Domain.Models;
using Saskaita.Repository;

namespace Saskaita.API.Application.Services
{
    public class InvoiceRequestValidator : AbstractValidator<InvoiceRequest>
    {
        public ICountryRepository _countryRepository;

        public InvoiceRequestValidator(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
            RuleFor(invoice => invoice.BuyerCompanyName).NotNull().NotEmpty();
            RuleFor(invoice => invoice.BuyerAddress).NotNull().NotEmpty();
            RuleFor(invoice => invoice.BuyerVatCode).NotNull().NotEmpty();
            RuleFor(invoice => invoice.BuyerCompanyCode).NotNull().NotEmpty();
            RuleFor(invoice => invoice.BuyerCountry).NotNull().NotEmpty();
            RuleFor(invoice => invoice.BuyerCountry).Must(SupportedCountry).WithMessage("Country not supported.");
            RuleFor(invoice => invoice.SellerCompanyName).NotNull().NotEmpty();
            RuleFor(invoice => invoice.SellerAddress).NotNull().NotEmpty();
            RuleFor(invoice => invoice.SellerVatCode).NotNull().NotEmpty();
            RuleFor(invoice => invoice.SellerCompanyCode).NotNull().NotEmpty();
            RuleFor(invoice => invoice.SellerCountry).NotNull().NotEmpty();
            RuleFor(invoice => invoice.SellerCountry).Must(SupportedCountry).WithMessage("Country not supported.");
            RuleFor(invoice => invoice.SellerBank).NotNull().NotEmpty();
            RuleFor(invoice => invoice.SellerBankAccountNumber).NotNull().NotEmpty();
            RuleFor(invoice => invoice.ServiceName).NotNull().NotEmpty();
            RuleFor(invoice => invoice.Units).GreaterThan(0).WithMessage("Units must be more than 0");
            RuleFor(invoice => invoice.UnitPrice).GreaterThan(0).WithMessage("Unit price must be more than 0");
        }

        private bool SupportedCountry(string? country)
        {
            return _countryRepository.GetSupportedCountryList().Contains(country);
        }

    }
}
