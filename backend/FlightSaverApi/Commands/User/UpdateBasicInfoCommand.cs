using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.User;

public class UpdateBasicInfoCommand : IRequest<EditedUserDTO>
{
    public int UserId { get; set; }
    public UpdateBasicInfoDTO UpdateBasicInfoDto { get; set; }
}