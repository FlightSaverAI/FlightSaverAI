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
    
    /// <summary>
    /// Retrieves a list of minimal flight details for a specific user.
    /// </summary>
    /// <remarks>
    /// This endpoint fetches basic flight details for the user identified by their user ID. 
    /// If the user ID is not provided, the method attempts to extract it from the claims in the HTTP context.
    /// </remarks>
    /// <param name="cancellation">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="userId">
    /// The optional ID of the user whose minimal flight details are being queried. 
    /// If not provided, the user's ID will be inferred from the claims in the HTTP context.
    /// </param>
    /// <returns>
    /// A list of minimal flight details for the user, or an appropriate HTTP response if an error occurs.
    /// </returns>
    /// <response code="200">Returns the list of minimal flights for the user.</response>
    /// <response code="401">If the user is not authorized to access this resource.</response>
    /// <response code="400">If the request is invalid (e.g., missing required data).</response>
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
    
    /// <summary>
    /// Retrieves a list of flights for a specific user.
    /// </summary>
    /// <remarks>
    /// This endpoint fetches detailed flight information for the user identified by their user ID. 
    /// If the user ID is not provided, the endpoint will attempt to extract it from the claims in the HTTP context.
    /// An optional `include` parameter can be used to specify related data (e.g., airlines, airports) to include in the response.
    /// </remarks>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="userId">
    /// The optional ID of the user whose flights are being queried. 
    /// If not provided, the user's ID will be inferred from the claims in the HTTP context.
    /// </param>
    /// <param name="include">
    /// A comma-separated string specifying related data to include in the response. 
    /// Supported values: "airports", "airlines", "aircrafts", "reviews".
    /// </param>
    /// <returns>
    /// A list of flights with optional related data, or an appropriate HTTP response if an error occurs.
    /// </returns>
    /// <response code="200">Returns the list of flights for the user.</response>
    /// <response code="401">If the user is not authorized to access this resource.</response>
    /// <response code="400">If the request is invalid (e.g., missing required data).</response>
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