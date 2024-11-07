using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FlightSaverApi.Commands.Auth;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
using FlightSaverApi.Models.UserModel;
using MediatR;

namespace FlightSaverApi.Controllers
{
    [Route("/Auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: /Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var command = new RegisterUserCommand { UserRegisterDTO = request };
            var token = await _mediator.Send(command);
            return Ok(new { token = token });
        }

        // POST: /Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var command = new LoginUserCommand { UserLoginDTO = request };
            var token = await _mediator.Send(command);
            return Ok(new { token = token });
        }
    }
}
