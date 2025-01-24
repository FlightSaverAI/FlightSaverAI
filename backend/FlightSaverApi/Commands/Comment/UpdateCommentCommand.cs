using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs.Post;
using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Comment;

[SwaggerExclude]
public class UpdateCommentCommand : IRequest<EditCommentDTO>
{
    public int Id { get; set; }
    
    public EditCommentDTO EditCommentDTO { get; set; }
    
    [JsonIgnore]
    public int UserId { get; set; }
}