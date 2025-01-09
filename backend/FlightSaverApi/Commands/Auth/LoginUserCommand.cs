using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.Auth;

[SwaggerExclude]
public class LoginUserCommand : IRequest<string>
{
    public UserLoginDTO UserLoginDTO { get; set; }
}