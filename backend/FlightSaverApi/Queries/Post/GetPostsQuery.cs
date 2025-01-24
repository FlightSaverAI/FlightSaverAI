using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Post;

public class GetPostsQuery : IRequest<IEnumerable<SocialPostDTO>>
{
    
}