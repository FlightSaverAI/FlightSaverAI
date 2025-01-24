using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Comment;

public class GetCommentsQuery : IRequest<IEnumerable<CommentDTO>>
{
    
}