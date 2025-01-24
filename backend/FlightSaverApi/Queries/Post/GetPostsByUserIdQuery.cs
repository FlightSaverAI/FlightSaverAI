using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Post;

public class GetPostsByUserIdQuery : IRequest<IEnumerable<SocialPostDTO>>
{
    public int UserId { get; set; }

    public GetPostsByUserIdQuery(int userId)
    {
        UserId = userId;
    }
}