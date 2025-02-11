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
            
            var loggedUserId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetPostsByUserIdQuery(userId.Value, loggedUserId, lastPostId, pageSize);
        
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
    /// Updates the content of an existing post, including location and text content.
    /// </summary>
    /// <param name="editSocialPostContentDTO">The DTO containing the updated content details.</param>
    /// <param name="cancellationToken">Token to cancel the request if needed.</param>
    /// <returns>The updated post details.</returns>
    /// <response code="200">Returns the updated post details.</response>
    /// <response code="401">If the user is not authorized to update the post.</response>
    /// <response code="404">If the post with the specified ID is not found.</response>
    [HttpPut("content")]
    public async Task<ActionResult<SocialPostDTO>> UpdatePostContent(EditSocialPostContentDTO editSocialPostContentDTO, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            var command = new UpdatePostContentCommand()
            {
                UserId = userId,
                EditPostContentDto = editSocialPostContentDTO
            };
            var post = await _mediator.Send(command, cancellationToken);
            return Ok(post);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    /// <summary>
    /// Updates the image of an existing post, either by uploading a new image or deleting the current one.
    /// </summary>
    /// <param name="editSocialPostImageDto">The DTO containing the image update details.</param>
    /// <param name="cancellationToken">Token to cancel the request if needed.</param>
    /// <returns>The updated post details.</returns>
    /// <response code="200">Returns the updated post details.</response>
    /// <response code="401">If the user is not authorized to update the post.</response>
    /// <response code="404">If the post with the specified ID is not found.</response>
    [HttpPut("image")]
    public async Task<ActionResult<SocialPostDTO>> UpdatePostImage(EditSocialPostImageDTO editSocialPostImageDto, CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);

            var command = new UpdatePostImageCommand
            {
                UserId = userId,
                EditPostImageDTO = editSocialPostImageDto
            };

            var post = await _mediator.Send(command, cancellationToken);
            return Ok(post);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
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