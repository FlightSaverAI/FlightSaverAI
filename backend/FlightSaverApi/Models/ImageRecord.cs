namespace FlightSaverApi.Models;

public class ImageRecord
{
    public int Id { get; set; }
    public string Url { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}