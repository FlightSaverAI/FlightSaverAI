using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Queries.Post;

public class GetPostQuery : IRequest<SocialPostDTO>
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public GetPostQuery(int id, int userId)
    {
        Id = id;
        UserId = userId;
    }
}