using FlightSaverApi.Models.UserModel;
using MediatR;

namespace FlightSaverApi.Commands.Auth;

public class LoginUserCommand : IRequest<string>
{
    public UserLoginDTO UserLoginDTO { get; set; }
}