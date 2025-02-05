using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.User;

public class SetUserRoleCommand : IRequest<Unit>
{
    public UpdateUserRoleDTO UpdateUserRoleDto { get; set; }
}