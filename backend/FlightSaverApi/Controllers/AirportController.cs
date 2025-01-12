using FlightSaverApi.Commands.Airport;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airport;
using FlightSaverApi.Queries.Airport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/airports")]
[ApiController]
[Authorize]
public class AirportController : ControllerBase
{
    private readonly IMediator _mediator;

    public AirportController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET: /airports
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AirportDTO>>> GetAirports(CancellationToken cancellationToken)
    {
        var query = new GetAirportsQuery();
        var airports = await _mediator.Send(query, cancellationToken);
        
        return Ok(airports);
    }

    /// <summary>
    /// Retrieves a list of minimal airport details.
    /// </summary>
    /// <remarks>
    /// This endpoint fetches basic details of airports, such as their ID and name, 
    /// for use in scenarios where only minimal data is required (e.g., dropdowns or summaries).
    /// The method uses a query handler to retrieve the data.
    /// </remarks>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A list of minimal airport details, or an appropriate HTTP response if an error occurs.
    /// </returns>
    /// <response code="200">Returns the list of minimal airport details.</response>
    /// <response code="401">If the user is not authorized to access this resource.</response>
    /// <response code="500">If an internal server error occurs while processing the request.</response>
    [HttpGet("minimal")]
    public async Task<ActionResult<IEnumerable<MinimalAirportDTO>>> GetMinimalAirports(CancellationToken cancellationToken)
    {
        var query = new GetMinimalAirportsQuery();
        var airports = await _mediator.Send(query, cancellationToken);
        
        return Ok(airports);
    }
    
    // GET: /airports/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AirportDTO>> GetAirport(int id, CancellationToken cancellationToken)
    {
        var query = new GetAirportQuery(id);
        var airport = await _mediator.Send(query, cancellationToken);

        return Ok(airport);
    }
    
    // PUT: /airports/{id}
    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> PutAirport(int id, UpdateAirportCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    // POST: /airports
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult<AirportDTO>> PostAirport(CreateAirportCommand command,
        CancellationToken cancellationToken)
    {
        var createdAirport = await _mediator.Send(command, cancellationToken);
        
        return CreatedAtAction(nameof(GetAirport), new { id = createdAirport.Id }, createdAirport);
    }
    
    // DELETE: /airports/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult> DeleteAirport(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteAirportCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}