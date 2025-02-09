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

    /// <summary>
    /// Retrieves user information for a specific user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <param name="userId">Optional query parameter to specify the user ID (if not provided, the logged-in user's ID is used).</param>
    /// <returns>The details of the specified user.</returns>
    [HttpGet("info")]
    public async Task<ActionResult<UserInfoDTO>> GetUserInfo(CancellationToken cancellationToken, int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetUserInfoQuery(userId.Value);
        
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
    
    /// <summary>
    /// Retrieves a paginated list of all users.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of users per page.</param>
    /// <param name="name">An optional name filter to search users by name.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>A paginated list of users.</returns>
    [HttpGet]
    public async Task<ActionResult<PagedUserResult>> GetUsers([FromQuery] int? pageNumber,
        [FromQuery] int? pageSize, [FromQuery] string? name, CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        var role = ClaimsHelper.GetUserTokenFromClaims(HttpContext.User);
        
        var query = new GetUsersQuery()
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Name = name,
            UserRole = role
        };
        
        var users = await _mediator.Send(query, cancellationToken);
        
        return Ok(users);
    }

    /// <summary>
    /// Retrieves a paginated list of friends for the logged-in user.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of friends per page.</param>
    /// <param name="name">An optional name filter to search friends by name.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>A paginated list of friends.</returns>
    [HttpGet("/friend")]
    public async Task<ActionResult<PagedUserResult>> GetFriends([FromQuery] int? pageNumber,
        [FromQuery] int? pageSize, [FromQuery] string? name, CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        var role = ClaimsHelper.GetUserTokenFromClaims(HttpContext.User);
        
        var query = new GetFriendsQuery()
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Name = name,
            UserRole = role
        };
        
        var users = await _mediator.Send(query, cancellationToken);
        
        return Ok(users);
    }
    
    /// <summary>
    /// Adds a new friend by their ID.
    /// </summary>
    /// <param name="friendId">The ID of the user to add as a friend.</param>
    /// <returns>No content if the friend was successfully added.</returns>
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

    /// <summary>
    /// Removes a friend by their ID.
    /// </summary>
    /// <param name="friendId">The ID of the user to remove from the friend list.</param>
    /// <returns>No content if the friend was successfully removed.</returns>
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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Sets the role for a user. Only accessible by users with the Admin role. 
    /// The only valid role values are "Admin" and "User".
    /// </summary>
    /// <param name="setUserRoleDto">The data transfer object containing the user ID and the role to be assigned. 
    /// Only the roles "Admin" and "User" are valid.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <response code="200">Successfully updated the user's role.</response>
    /// <response code="400">The role is invalid, the request body is incorrect, or the provided role is not "Admin" or "User".</response>
    /// <response code="404">The user with the specified ID was not found.</response>
    /// <response code="401">Unauthorized access due to insufficient permissions. The Admin role is required.</response>
    /// <response code="500">An unexpected error occurred on the server.</response>
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("user-role")]
    public async Task<IActionResult> SetUserRole([FromForm]UpdateUserRoleDTO updateUserRoleDto, CancellationToken cancellationToken)
    {
        try
        {
            var command = new SetUserRoleCommand
            {
                UpdateUserRoleDto = updateUserRoleDto
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