using FlightSaverApi.DTOs.Post;
using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Comment;

public class CreateCommentCommand : IRequest<NewCommentDTO>
{
    public int UserId { get; set; }
    public NewCommentDTO Comment { get; set; }
}