using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FlightSaverApi.DTOs.User;

public class EditUserDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public IFormFile? ProfilePictureImage { get; set; }
    public IFormFile? BackgroundPictureImage { get; set; }
}