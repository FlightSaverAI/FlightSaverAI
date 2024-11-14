using FlightSaverApi.Attributes;
using FlightSaverApi.Models.UserModel;
using MediatR;

namespace FlightSaverApi.Commands.Auth;

[SwaggerExclude]
public class LoginUserCommand : IRequest<string>
{
    public UserLoginDTO UserLoginDTO { get; set; }
}