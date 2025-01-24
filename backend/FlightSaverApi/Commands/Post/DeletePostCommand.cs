using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Post;

public class DeletePostCommand : IRequest<Unit>
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public int UserId { get; set; }
}