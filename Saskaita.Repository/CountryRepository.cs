namespace Saskaita.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public bool IsCountryInEU(string? countryCode)
        {
            return countryCode switch
            {
                "LT" or "FR" => true,
                "CA" or "JP" => false,
                _ => false,
            };
        }

        public int GetCountryVatRate(string? countryCode)
        {
            return countryCode switch
            {
                "LT" => 21,
                "FR" => 20,
                "CA" => 5,
                "JP" => 10,
                _ => 0,
            };
        }

        public List<string> GetSupportedCountryList()
        {
            return new List<string> { "LT", "CA", "FR", "JP"};
        }
    }
}
