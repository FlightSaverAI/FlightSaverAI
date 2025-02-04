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

        /// <summary>
        /// Registers a new user and returns a JWT token upon successful registration.
        /// </summary>
        /// <param name="request">The user registration details.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the JWT token if registration is successful,
        /// or a 500 status code with an error message if an exception occurs.
        /// </returns>
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

        /// <summary>
        /// Authenticates a user and returns a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="request">The user login details.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the JWT token if authentication is successful,
        /// or a 500 status code with an error message if an exception occurs.
        /// </returns>
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
