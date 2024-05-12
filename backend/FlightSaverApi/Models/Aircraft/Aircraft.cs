namespace FlightSaverApi.Models.Aircraft
{
    public class Aircraft
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IataCode { get; set; }
        public string? IcaoCode { get; set; }
        public string? RegNumber { get; set; }
        public string? Secret { get; set; }
    }
}
