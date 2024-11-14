using FlightSaverApi.Attributes;
using FlightSaverApi.Models.UserModel;
using MediatR;

namespace FlightSaverApi.Commands.Auth;

[SwaggerExclude]
public class RegisterUserCommand : IRequest<string>
{
    public UserRegisterDTO UserRegisterDTO { get; set; }
}