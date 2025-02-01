using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.User;

public class UpdatePicturesCommand : IRequest<EditedUserDTO>
{
    public int UserId { get; set; }
    public UpdatePicturesDTO UpdatePicturesDto { get; set; }
}