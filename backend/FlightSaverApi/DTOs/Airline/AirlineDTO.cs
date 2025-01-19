namespace FlightSaverApi.DTOs.Airline
{
    public class AirlineDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IataCode { get; set; }

        public string IcaoCode { get; set; }

        public string Country { get; set; }

        public string? LogoUrl { get; set; }
    }
}

