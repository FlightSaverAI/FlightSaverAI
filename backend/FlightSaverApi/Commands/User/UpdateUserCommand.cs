using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.User;

public class UpdateUserCommand : IRequest<EditedUserDTO>
{
    public int UserId { get; set; }
    public EditUserDTO EditUserDto { get; set; }
}