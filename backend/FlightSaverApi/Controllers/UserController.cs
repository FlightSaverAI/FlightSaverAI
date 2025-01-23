using FlightSaverApi.Commands.User;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Helpers;
using FlightSaverApi.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/user")]
[Authorize(Policy = "RequireUserRole")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<EditUserDTO>> GetUserInfo(CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetEditUserQuery(userId);
        
            var user = await _mediator.Send(query, cancellationToken);
        
            return Ok(user);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(EditUserDTO editUserDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var command = new UpdateUserCommand
            {
                UserId = userId,
                EditUserDto = editUserDto
            };
            
            await _mediator.Send(command, cancellationToken);
            
            return Ok();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}