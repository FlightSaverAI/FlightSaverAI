using FlightSaverApi.Commands.Post;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Helpers;
using FlightSaverApi.Queries.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/post")]
[Authorize(Policy = "RequireUserRole")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SocialPostDTO>>> GetPosts(CancellationToken cancellationToken)
    {
        var query = new GetPostsQuery();
        var posts = await _mediator.Send(query, cancellationToken);
        
        return Ok(posts);
    }

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<SocialPostDTO>>> GetPostsByUserId(CancellationToken cancellationToken,
        int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetPostsByUserIdQuery(userId.Value);
        
            var posts = await _mediator.Send(query, cancellationToken);
        
            return Ok(posts);
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SocialPostDTO>> GetPost([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetPostQuery(id);
        var post = await _mediator.Send(query, cancellationToken);
        
        return Ok(post);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost(EditSocialPostDTO editSocialPostDTO,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var command = new UpdatePostCommand()
            {
                UserId = userId,
                Id = editSocialPostDTO.Id,
                EditSocialPostDTO = editSocialPostDTO
            };
        
            var post = await _mediator.Send(command, cancellationToken);
        
            return Ok(post);
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
    public async Task<ActionResult<SocialPostDTO>> CreatePost([FromBody] CreatePostCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            command.Post.UserId = userId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPost = await _mediator.Send(command, cancellationToken);

            return Ok(createdPost);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
            var command = new DeletePostCommand() { Id = id, UserId = userId };
            
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
    public async Task<IActionResult> LikePost([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new LikePostCommand { PostId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Post not found.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("{id:int}/unlike")]
    public async Task<IActionResult> UnlikePost([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new UnlikePostCommand { PostId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Post not found.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}