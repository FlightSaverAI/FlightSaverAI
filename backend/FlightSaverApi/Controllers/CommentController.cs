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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByPostId(int postId,
        CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetCommentsByPostIdQuery(postId, userId);
        
        var comments = await _mediator.Send(query, cancellationToken);
        
        return Ok(comments);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment(EditCommentDTO editCommentDTO,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
            var command = new UpdateCommentCommand()
            {
                UserId = userId,
                Id = editCommentDTO.Id,
                EditCommentDTO = editCommentDTO
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
    
    [HttpPost]
    public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            command.Comment.UserId = userId;

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
    
    [HttpDelete("{id:int}")]
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
    
    [HttpPost("{id:int}/like")]
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
    
    [HttpPost("{id:int}/unlike")]
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