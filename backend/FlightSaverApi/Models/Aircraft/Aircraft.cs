using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.Aircraft
{
    public class Aircraft
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [StringLength(3, MinimumLength = 2)]
        public string? IataCode { get; set; }
        [StringLength(4, MinimumLength = 3)]
        public string? IcaoCode { get; set; }
        public string? RegNumber { get; set; }
        public string? Secret { get; set; }
    }
}
