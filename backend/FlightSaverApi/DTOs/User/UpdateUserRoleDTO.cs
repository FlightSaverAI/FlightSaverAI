using System.Text.Json.Serialization;
using FlightSaverApi.Enums;

namespace FlightSaverApi.DTOs.User;

public class UpdateUserRoleDTO
{
    public int Id { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }
}