using System.Security.Claims;
using FlightSaverApi.Commands.Flight;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Flight;
using FlightSaverApi.Queries.Flight;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/flights")]
[ApiController]
[Authorize(Policy = "RequireUserRole")]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET: /flights
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightDTO>>> GetFlights(CancellationToken cancellationToken)
    {
        var query = new GetFlightsQuery();
        var flights = await _mediator.Send(query, cancellationToken);
        
        return Ok(flights);
    }
    
    // GET: /flights/user/minimal/{userId?}
    [HttpGet("user/minimal")]
    public async Task<ActionResult<IEnumerable<MinimalFlightDTO>>> GetMinimalFlightsByUser(CancellationToken cancellation, int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);

            var query = new GetMinimalFlightsByUserQuery(userId.Value);
            var minimalFlights = await _mediator.Send(query, cancellation);

            return Ok(minimalFlights);
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
    
    // GET: /flights/user/{userId?}
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<FlightDTO>>> GetFlightsByUser(CancellationToken cancellationToken, int? userId = null, string? include = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetFlightsByUserQuery(userId.Value, include);
            var flights = await _mediator.Send(query, cancellationToken);
            
            return Ok(flights);
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
    
    // GET: /flights/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightDTO>> GetFlight(int id, CancellationToken cancellationToken)
    {
        var query = new GetFlightQuery(id);
        var flight = await _mediator.Send(query, cancellationToken);
        
        return Ok(flight);
    }
    
    // PUT: /flights/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutFlight(int id, UpdateFlightCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id || !ModelState.IsValid) return BadRequest();
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    // POST: /flights
    [HttpPost]
    public async Task<ActionResult<FlightDTO>> PostFlight(CreateFlightCommand command,
        CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var createdFlight = await _mediator.Send(command, cancellationToken);
        
        return CreatedAtAction(nameof(GetFlight), new { id = createdFlight.Id }, createdFlight);
    }
    
    // DELETE: /flights/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<FlightDTO>> DeleteFlight(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteFlightCommand { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}