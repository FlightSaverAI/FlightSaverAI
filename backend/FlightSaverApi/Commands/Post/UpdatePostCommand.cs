using FlightSaverApi.DTOs.Post;
using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Post;

public class UpdatePostCommand : IRequest<EditSocialPostDTO>
{
    public int Id { get; set; }
    
    public EditSocialPostDTO EditSocialPostDTO { get; set; }
    
    [JsonIgnore]
    public int UserId { get; set; }
}