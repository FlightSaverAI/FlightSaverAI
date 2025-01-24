using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Post;

public class GetPostQuery : IRequest<SocialPostDTO>
{
    public int Id { get; set; }

    public GetPostQuery(int id)
    {
        Id = id;
    }
}