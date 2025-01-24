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

    [HttpGet("info")]
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
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FriendDTO>>> GetUsers(CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetUsersQuery() {UserId = userId};
        
        var users = await _mediator.Send(query, cancellationToken);
        
        return Ok(users);
    }

    [HttpGet("/friend")]
    public async Task<ActionResult<IEnumerable<FriendDTO>>> GetFriends(CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetFriendsQuery() {UserId = userId};
        
        var users = await _mediator.Send(query, cancellationToken);
        
        return Ok(users);
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
    
    [HttpPost("/friend/add")]
    public async Task<IActionResult> AddFriend(int friendId)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var result = await _mediator.Send(new AddFriendCommand
            {
                UserId = userId,
                FriendId = friendId
            });

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("/friend/remove")]
    public async Task<IActionResult> RemoveFriend(int friendId)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var result = await _mediator.Send(new RemoveFriendCommand
            {
                UserId = userId,
                FriendId = friendId
            });

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}