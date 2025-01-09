using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Commands.Auth;

[SwaggerExclude]
public class RegisterUserCommand : IRequest<string>
{
    public UserRegisterDTO UserRegisterDTO { get; set; }
}