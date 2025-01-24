using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Comment;

public class GetCommentQuery : IRequest<CommentDTO>
{
    public int Id { get; set; }

    public GetCommentQuery(int id)
    {
        Id = id;
    }
}