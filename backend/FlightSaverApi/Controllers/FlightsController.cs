using FlightSaverApi.Commands.Flight;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Queries.Flight;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("Flights")]
[ApiController]
[Authorize]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET: /Flights
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightDTO>>> GetFlights(CancellationToken cancellationToken)
    {
        var query = new GetFlightsQuery();
        var flights = await _mediator.Send(query, cancellationToken);
        
        return Ok(flights);
    }
    
    // GET: /Flights/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightDTO>> GetFlight(int id, CancellationToken cancellationToken)
    {
        var query = new GetFlightQuery(id);
        var flight = await _mediator.Send(query, cancellationToken);
        
        if (flight == null) return NotFound();
        
        return Ok(flight);
    }
    
    // PUT: /Flights/{id}
    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> PutFlight(int id, UpdateFlightCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id) return BadRequest();
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    // POST: /Flights
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult<FlightDTO>> PostFlight(CreateFlightCommand command,
        CancellationToken cancellationToken)
    {
        var createdFlight = await _mediator.Send(command, cancellationToken);
        
        return CreatedAtAction(nameof(GetFlight), new { id = createdFlight.Id }, createdFlight);
    }
    
    // DELETE: /Flights/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult<FlightDTO>> DeleteFlight(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteFlightCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}