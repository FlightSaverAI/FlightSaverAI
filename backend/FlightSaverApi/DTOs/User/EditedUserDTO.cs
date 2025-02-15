namespace FlightSaverApi.DTOs.User;

public class EditedUserDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? BackgroundPictureUrl { get; set; }
}