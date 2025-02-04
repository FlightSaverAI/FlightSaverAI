using FlightSaverApi.Commands.Comment;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Helpers;
using FlightSaverApi.Queries.Comment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/comment")]
[Authorize(Policy = "RequireUserRole")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves comments for a specific post.
    /// </summary>
    /// <param name="postId">The ID of the post for which comments are retrieved.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>
    /// Returns an <see cref="ActionResult{T}"/> containing a list of <see cref="CommentDTO"/> 
    /// if successful, or an appropriate error response.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByPostId(int postId,
        CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetCommentsByPostIdQuery(postId, userId);
        
        var comments = await _mediator.Send(query, cancellationToken);
        
        return Ok(comments);
    }
    
    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="editCommentDto">The data transfer object containing the updated comment.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An updated comment.</returns>
    /// <response code="200">Successfully updated comment.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">User is unauthorized.</response>
    /// <response code="404">Comment not found.</response>
    [HttpPut]
    [ProducesResponseType(typeof(EditCommentDTO), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [HttpPut]
    public async Task<IActionResult> UpdateComment(EditCommentDTO editCommentDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
            var command = new UpdateCommentCommand()
            {
                UserId = userId,
                Id = editCommentDto.id,
                EditCommentDTO = editCommentDto
            };
        
            var comment = await _mediator.Send(command, cancellationToken);
        
            return Ok(comment);
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
    /// Creates a new comment.
    /// </summary>
    /// <param name="comment">The new comment data.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>Returns the created comment.</returns>
    /// <response code="200">Successfully created the comment.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CommentDTO), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CommentDTO>> CreateComment(NewCommentDTO comment,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);

            var command = new CreateCommentCommand()
            {
                UserId = userId,
                Comment = comment
            };
            
            command.UserId = userId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdComment = await _mediator.Send(command, cancellationToken);

            return Ok(createdComment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>Returns a 204 No Content response if successful.</returns>
    /// <response code="204">Comment deleted successfully.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">User is unauthorized to delete this comment.</response>
    /// <response code="404">Comment not found.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteComment([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
            var command = new DeleteCommentCommand() { Id = id, UserId = userId };
            
            await _mediator.Send(command, cancellationToken);
            
            return NoContent();
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
    /// Likes a comment.
    /// </summary>
    /// <param name="id">The ID of the comment to like.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>Returns a 204 No Content response if successful.</returns>
    /// <response code="204">Comment liked successfully.</response>
    /// <response code="404">Comment not found.</response>
    /// <response code="400">Invalid request.</response>
    [HttpPost("{id:int}/like")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> LikeComment([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new LikeCommentCommand { CommentId = id, UserId = userId };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Comment not found.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Removes a like from a comment.
    /// </summary>
    /// <param name="id">The ID of the comment to unlike.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>Returns a 204 No Content response if successful.</returns>
    /// <response code="204">Comment unliked successfully.</response>
    /// <response code="404">Comment not found.</response>
    /// <response code="400">Invalid request.</response>
    [HttpPost("{id:int}/unlike")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UnlikeComment([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new UnlikeCommentCommand { CommentId = id, UserId = userId };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Comment not found.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}