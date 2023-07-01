namespace Saskaita.Repository
{
    public interface ICountryRepository
    {
        bool IsCountryInEU(string? countryCode);
        int GetCountryVatRate(string? countryCode);
        List<string> GetSupportedCountryList();
    }
}
