using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs.Post;
using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Comment;

[SwaggerExclude]
public class DeleteCommentCommand : IRequest<Unit>
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public int UserId { get; set; }
}