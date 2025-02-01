using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.User;

public class UpdatePasswordCommand : IRequest<EditedUserDTO>
{
    public int UserId { get; set; }
    public UpdatePasswordDTO UpdatePasswordDto { get; set; }
}