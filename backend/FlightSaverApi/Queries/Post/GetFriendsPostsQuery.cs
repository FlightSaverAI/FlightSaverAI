using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Post;

public class GetFriendsPostsQuery : IRequest<IEnumerable<SocialPostDTO>>
{
    public int UserId { get; set; }

    public GetFriendsPostsQuery(int userId)
    {
        UserId = userId;
    }
}