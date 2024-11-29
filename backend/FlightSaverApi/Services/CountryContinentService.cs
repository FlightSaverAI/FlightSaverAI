using System.Text.Json;
using FlightSaverApi.Enums;

namespace FlightSaverApi.Services
{
    public class CountryContinentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountryContinentService> _logger;

        public CountryContinentService(HttpClient httpClient, ILogger<CountryContinentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Dictionary<string, Continent>> FetchCountryToContinentMappingAsync(CancellationToken cancellationToken = default)
        {
            const string url = "https://restcountries.com/v3.1/all";

            try
            {
                _logger.LogInformation("Fetching country data from {Url}", url);

                // Stream response to handle large payloads
                using var responseStream = await _httpClient.GetStreamAsync(url, cancellationToken);
                var countries = await JsonSerializer.DeserializeAsync<List<RestCountry>>(responseStream, cancellationToken: cancellationToken);

                if (countries == null)
                {
                    _logger.LogWarning("Deserialization returned null for country data.");
                    return new Dictionary<string, Continent>();
                }

                var mapping = countries
                    .Where(country => !string.IsNullOrEmpty(country.Region))
                    .ToDictionary(
                        country => country.Name.Common,
                        country => MapToContinentEnum(country.Region)
                    );

                _logger.LogInformation("Successfully fetched and mapped {Count} countries to continents.", mapping.Count);
                return mapping;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while fetching country data.");
                throw new Exception("Failed to fetch country data due to network issues.", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing country data.");
                throw new Exception("Failed to deserialize country data.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                throw;
            }
        }

        private Continent MapToContinentEnum(string region)
        {
            return region switch
            {
                "Europe" => Continent.Europe,
                "Asia" => Continent.Asia,
                "Antarctic" => Continent.Antarctic,
                "North America" => Continent.NorthAmerica,
                "South America" => Continent.SouthAmerica,
                "Oceania" => Continent.Oceania,
                "Africa" => Continent.Africa,
                _ => Continent.Unknown // Fallback for unrecognized regions
            };
        }
    }

    public class RestCountry
    {
        public Name Name { get; set; }
        public string Region { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
    }
}
