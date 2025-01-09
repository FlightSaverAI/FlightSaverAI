using FlightSaverApi.Commands.Airline;
using FlightSaverApi.DTOs;
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
    
    // GET: /airlines/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AirlineDTO>> GetAirline(int id, CancellationToken cancellationToken)
    {
        var query = new GetAirlineQuery(id);
        var airline = await _mediator.Send(query, cancellationToken);

        if (airline == null)
        {
            return NotFound();
        }
        
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