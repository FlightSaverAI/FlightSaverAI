using FlightSaverApi.Models.UserModel;
using MediatR;

namespace FlightSaverApi.Commands.Auth;

public class RegisterUserCommand : IRequest<string>
{
    public UserRegisterDTO UserRegisterDTO { get; set; }
}