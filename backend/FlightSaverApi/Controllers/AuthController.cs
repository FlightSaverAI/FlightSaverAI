using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FlightSaverApi.Commands.Auth;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
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
            try
            {
                var command = new RegisterUserCommand { UserRegisterDTO = request };
                var token = await _mediator.Send(command);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", details = ex.Message });
            }
        }

        // POST: /Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            try
            {
                var command = new LoginUserCommand { UserLoginDTO = request };
                var token = await _mediator.Send(command);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", details = ex.Message });
            }

        }
    }
}
