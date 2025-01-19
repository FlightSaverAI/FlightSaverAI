using Microsoft.AspNetCore.Mvc;
using FlightSaverApi.Commands.Auth;
using FlightSaverApi.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace FlightSaverApi.Controllers
{
    [Route("/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: /auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            try
            {
                var command = new RegisterUserCommand { UserRegisterDTO = request };
                var token = await _mediator.Send(command);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", details = ex.Message });
            }
        }

        // POST: /auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            try
            {
                var command = new LoginUserCommand { UserLoginDTO = request };
                var token = await _mediator.Send(command);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", details = ex.Message });
            }

        }
    }
}
