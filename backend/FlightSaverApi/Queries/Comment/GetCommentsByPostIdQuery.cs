using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Comment;

public class GetCommentsByPostIdQuery : IRequest<IEnumerable<CommentDTO>>
{
    public int PostId { get; set; }
    public int UserId { get; set; }

    public GetCommentsByPostIdQuery(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}