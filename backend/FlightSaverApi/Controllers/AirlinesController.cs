using FlightSaverApi.Commands.Airline;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airline;
using FlightSaverApi.Queries.Airline;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/airlines")]
[ApiController]
[Authorize]
public class AirlinesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AirlinesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET: /airlines
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AirlineDTO>>> GetAirlines(CancellationToken cancellationToken)
    {
        var query = new GetAirlinesQuery();
        var airlines =  await _mediator.Send(query, cancellationToken);
        
        return Ok(airlines);
    }
    
    /// <summary>
    /// Retrieves a list of minimal airline details.
    /// </summary>
    /// <remarks>
    /// This endpoint fetches basic details of airlines, such as their ID and name, 
    /// for use in scenarios where only minimal data is required (e.g., dropdowns or summaries).
    /// The method uses a query handler to retrieve the data.
    /// </remarks>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A list of minimal airline details, or an appropriate HTTP response if an error occurs.
    /// </returns>
    /// <response code="200">Returns the list of minimal airline details.</response>
    /// <response code="401">If the user is not authorized to access this resource.</response>
    /// <response code="500">If an internal server error occurs while processing the request.</response>
    [HttpGet("minimal")]
    public async Task<ActionResult<MinimalAirlineDTO>> GetMinimalAirlines(CancellationToken cancellationToken)
    {
        var query = new GetMinimalAirlinesQuery();
        var airlines =  await _mediator.Send(query, cancellationToken);
        
        return Ok(airlines);
    }
    
    // GET: /airlines/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AirlineDTO>> GetAirline(int id, CancellationToken cancellationToken)
    {
        var query = new GetAirlineQuery(id);
        var airline = await _mediator.Send(query, cancellationToken);

        return Ok(airline);
    }
    
    // PUT: /airlines/{id}
    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> PutAirline(int id, UpdateAirlineCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    // POST: /airlines
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult<AirlineDTO>> PostAirline(CreateAirlineCommand command,
        CancellationToken cancellationToken)
    {
        var createdAirline = await _mediator.Send(command, cancellationToken);
        
        return CreatedAtAction(nameof(GetAirline), new { id = createdAirline.Id }, createdAirline);
    }
    
    // DELETE: /airlines/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult> DeleteAirline(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteAirlineCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}