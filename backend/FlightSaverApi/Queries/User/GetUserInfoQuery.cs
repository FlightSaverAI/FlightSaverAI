using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Queries.User;

public class GetUserInfoQuery : IRequest<UserInfoDTO>
{
    public int Id { get; set; }

    public GetUserInfoQuery(int id)
    {
        Id = id;
    }
}