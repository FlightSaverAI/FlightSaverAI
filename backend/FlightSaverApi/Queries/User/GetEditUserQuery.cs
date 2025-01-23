using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Queries.User;

public class GetEditUserQuery : IRequest<EditUserDTO>
{
    public int Id { get; set; }

    public GetEditUserQuery(int id)
    {
        Id = id;
    }
}