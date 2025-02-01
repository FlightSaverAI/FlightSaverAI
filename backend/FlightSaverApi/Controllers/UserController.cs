using FlightSaverApi.Commands.User;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Helpers;
using FlightSaverApi.Queries.User;
using FlightSaverApi.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

/// <summary>
/// Controller for managing user updates.
/// </summary>
[ApiController]
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
    public async Task<ActionResult<UserInfoDTO>> GetUserInfo(CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetUserInfoQuery(userId);
        
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
    public async Task<ActionResult<PagedUserResult>> GetUsers([FromQuery] int? pageNumber,
        [FromQuery] int? pageSize, CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetUsersQuery()
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var users = await _mediator.Send(query, cancellationToken);
        
        return Ok(users);
    }

    [HttpGet("/friend")]
    public async Task<ActionResult<PagedUserResult>> GetFriends([FromQuery] int? pageNumber,
        [FromQuery] int? pageSize, CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetFriendsQuery()
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var users = await _mediator.Send(query, cancellationToken);
        
        return Ok(users);
    }
    
    [HttpPut]
    public async Task<ActionResult<EditedUserDTO>> UpdateUser(EditUserDTO editUserDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var command = new UpdateUserCommand
            {
                UserId = userId,
                EditUserDto = editUserDto
            };
            
            var user = await _mediator.Send(command, cancellationToken);
            
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
    
    /// <summary>
    /// Updates the basic user information (username and email).
    /// </summary>
    /// <param name="updateBasicInfoDto">The DTO containing the new username and email.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>An <see cref="IActionResult"/> containing the updated user details.</returns>
    /// <response code="200">Returns the updated user details.</response>
    /// <response code="400">If the provided data is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPut("basic-info")]
    public async Task<IActionResult> UpdateBasicInfo(UpdateBasicInfoDTO updateBasicInfoDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new UpdateBasicInfoCommand
            {
                UserId = userId,
                UpdateBasicInfoDto = updateBasicInfoDto
            };
            
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
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
    
    /// <summary>
    /// Updates the user's profile and background pictures.
    /// </summary>
    /// <param name="updatePicturesDto">The DTO containing the new images for profile and/or background.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>An <see cref="IActionResult"/> containing the updated user details.</returns>
    /// <response code="200">Returns the updated user details.</response>
    /// <response code="400">If the provided data is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPut("pictures")]
    public async Task<IActionResult> UpdatePictures(UpdatePicturesDTO updatePicturesDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new UpdatePicturesCommand
            {
                UserId = userId,
                UpdatePicturesDto = updatePicturesDto
            };
            
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
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
    
    /// <summary>
    /// Updates the user's password.
    /// </summary>
    /// <param name="updatePasswordDto">The DTO containing the new password.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>An <see cref="IActionResult"/> containing the updated user details.</returns>
    /// <response code="200">Returns the updated user details.</response>
    /// <response code="400">If the provided data is invalid.</response>
    /// <response code="401">If the user is unauthorized.</response>
    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new UpdatePasswordCommand
            {
                UserId = userId,
                UpdatePasswordDto = updatePasswordDto
            };
            
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
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