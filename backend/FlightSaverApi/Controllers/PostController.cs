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

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<SocialPostDTO>>> GetPosts(CancellationToken cancellationToken)
    // {
    //     var query = new GetPostsQuery();
    //     var posts = await _mediator.Send(query, cancellationToken);
    //     
    //     return Ok(posts);
    // }

    /// <summary>
    /// Fetch posts created by a specific user, with optional pagination.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <param name="lastPostId">Optional query parameter to specify the ID of the last post for pagination.</param>
    /// <param name="pageSize">Optional query parameter to specify the number of posts per page (default is 10).</param>
    /// <param name="userId">Optional query parameter to specify the user ID (if not provided, the logged-in user's ID is used).</param>
    /// <returns>A list of posts created by the user.</returns>
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<SocialPostDTO>>> GetPostsByUserId(CancellationToken cancellationToken, 
        [FromQuery] int? lastPostId, [FromQuery] int pageSize = 10, int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetPostsByUserIdQuery(userId.Value, lastPostId, pageSize);
        
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
    
    /// <summary>
    /// Fetch posts from friends and logged user, with optional pagination.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <param name="lastPostId">Optional query parameter to specify the ID of the last post for pagination.</param>
    /// <param name="pageSize">Optional query parameter to specify the number of posts per page (default is 10).</param>
    /// <returns>A list of posts from friends and logged user.</returns>
    [HttpGet("friend")]
    public async Task<ActionResult<IEnumerable<SocialPostDTO>>> GetFriendsPosts(CancellationToken cancellationToken,
        [FromQuery] int? lastPostId = null, [FromQuery] int pageSize = 10)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);

            var query = new GetFriendsPostsQuery(userId, lastPostId, pageSize);

            var posts = await _mediator.Send(query, cancellationToken);

            return Ok(posts);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Fetch a single post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>The details of a specific post.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SocialPostDTO>> GetPost([FromRoute] int id, CancellationToken cancellationToken)
    {
        var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
        
        var query = new GetPostQuery(id, userId);
        var post = await _mediator.Send(query, cancellationToken);
        
        return Ok(post);
    }

    /// <summary>
    /// Update an existing post.
    /// </summary>
    /// <param name="editSocialPostDTO">The data for the post update.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>The details of the updated post.</returns>
    [HttpPut]
    public async Task<ActionResult<SocialPostDTO>> UpdatePost(EditSocialPostDTO editSocialPostDTO,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var command = new UpdatePostCommand()
            {
                UserId = userId,
                Id = editSocialPostDTO.id,
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

    /// <summary>
    /// Create a new post.
    /// </summary>
    /// <param name="post">The post data to be created.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>The details of the created post.</returns>
    [HttpPost]
    public async Task<ActionResult<SocialPostDTO>> CreatePost(NewPostDTO post,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);

            var command = new CreatePostCommand()
            {
                UserId = userId,
                Post = post
            };
            
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

    /// <summary>
    /// Delete a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>No content if the post was successfully deleted.</returns>
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
    
    /// <summary>
    /// Like a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to like.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>No content if the post was successfully liked.</returns>
    [HttpPost("{id:int}/like")]
    public async Task<IActionResult> LikePost([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new LikePostCommand { PostId = id , UserId = userId };
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
    
    /// <summary>
    /// Unlike a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to unlike.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>No content if the post was successfully unliked.</returns>
    [HttpPost("{id:int}/unlike")]
    public async Task<IActionResult> UnlikePost([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new UnlikePostCommand { PostId = id, UserId = userId };
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