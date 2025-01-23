namespace FlightSaverApi.Services
{
    public class CountryContinentService
    {
        private readonly HttpClient _httpClient;

        public CountryContinentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        private readonly Dictionary<string, string> _countryContinentCache = new();

        public async Task<string?> GetContinentByCountryNameAsync(string countryName, CancellationToken cancellationToken = default)
        {
            if (_countryContinentCache.TryGetValue(countryName, out var cachedContinent))
                return cachedContinent;

            try
            {
                var continent = await FetchContinentFromApiAsync(countryName, cancellationToken);
                if (!string.IsNullOrEmpty(continent))
                    _countryContinentCache[countryName] = continent;

                return continent;
            }
            catch (Exception ex)
            {
                return "Error fetching continent from api";
            }

        }

        public async Task<string?> FetchContinentFromApiAsync(string countryName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentException("Country name cannot be null or empty.", nameof(countryName));

            try
            {
                var url = $"https://restcountries.com/v3.1/name/{countryName}";

                var response = await _httpClient.GetAsync(url, cancellationToken);

                response.EnsureSuccessStatusCode();

                var countryData = await response.Content.ReadFromJsonAsync<List<RestCountryResponse>>(cancellationToken: cancellationToken);

                return countryData?.FirstOrDefault()?.Region;  // This should work fine now
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Error fetching data for country {countryName}: {ex.Message}");
                return null;
            }
        }
    }

    public class RestCountryResponse
    {
        public CountryName Name { get; set; }
        public string Region { get; set; }
    }

    public class CountryName
    {
        public string Common { get; set; }
        public string Official { get; set; }
        public Dictionary<string, NativeName> NativeName { get; set; }
    }

    public class NativeName
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }
}
