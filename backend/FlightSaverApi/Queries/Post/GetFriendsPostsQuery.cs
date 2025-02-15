using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Post;

public class GetFriendsPostsQuery : IRequest<IEnumerable<SocialPostDTO>>
{
    public int UserId { get; }
    public int? LastPostId { get; }
    public int PageSize { get; }

    public GetFriendsPostsQuery(int userId, int? lastPostId, int pageSize)
    {
        UserId = userId;
        LastPostId = lastPostId;
        PageSize = pageSize;
    }
}