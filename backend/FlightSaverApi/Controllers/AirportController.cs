using FlightSaverApi.Commands.Airport;
using FlightSaverApi.DTOs;
using FlightSaverApi.Queries.Airport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/Airports")]
[ApiController]
[Authorize]
public class AirportController : ControllerBase
{
    private readonly IMediator _mediator;

    public AirportController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET: /Airports
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AirportDTO>>> GetAirports(CancellationToken cancellationToken)
    {
        var query = new GetAirportsQuery();
        var airports = await _mediator.Send(query, cancellationToken);
        
        return Ok(airports);
    }
    
    // GET: /Airports/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AirportDTO>> GetAirport(int id, CancellationToken cancellationToken)
    {
        var query = new GetAirportQuery(id);
        var airport = await _mediator.Send(query, cancellationToken);
        
        if(airport == null) return NotFound();
        
        return Ok(airport);
    }
    
    // PUT: /Airports/{id}
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
    
    // POST: /Airports
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult<AirportDTO>> PostAirport(CreateAirportCommand command,
        CancellationToken cancellationToken)
    {
        var createdAirport = await _mediator.Send(command, cancellationToken);
        
        return CreatedAtAction(nameof(GetAirport), new { id = createdAirport.Id }, createdAirport);
    }
    
    // DELETE: /Airports/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult> DeleteAirport(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteAirportCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}