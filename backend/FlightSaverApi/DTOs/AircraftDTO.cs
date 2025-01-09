namespace FlightSaverApi.DTOs
{
    public class AircraftDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IataCode { get; set; }

        public string IcaoCode { get; set; }

        public string RegNumber { get; set; }

        public string? AircraftUrl { get; set; }

        public int AirlineId { get; set; }
    }
}
