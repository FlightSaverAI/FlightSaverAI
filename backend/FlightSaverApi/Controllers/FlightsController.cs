using FlightSaverApi.Commands.Flight;
using FlightSaverApi.DTOs.Flight;
using FlightSaverApi.Helpers;
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
    
    /// <summary>
    /// Creates a new flight for the authenticated user with the provided details.
    /// </summary>
    /// <remarks>
    /// This endpoint allows an authenticated user to create a new flight record by submitting flight details such as departure and arrival airports, flight number, aircraft information, ticket type, and more.
    ///     The `userId` will automatically be assigned from the authenticated user's claims.
    ///     If the input data is invalid, the request will return an error.
    /// </remarks>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="command">
    ///     The command includes all relevant information such as flight details, airline, aircraft, and ticket information.
    /// </param>
    /// <returns>
    /// A newly created flight object with all the details provided, or an appropriate HTTP response in case of an error.
    /// </returns>
    /// <response code="201">Returns the created flight details.</response>
    /// <response code="400">If the request body is invalid (e.g., missing required fields).</response>
    /// <response code="401">If the user is not authorized to create a flight.</response>
    /// <response code="500">If an unexpected server error occurs during the creation process.</response>
    /// <notes>
    /// <note>
    /// The user ID will be automatically included from the authenticated user's claims.
    /// </note>
    /// <note>
    /// Ensure that the flight details are provided correctly as per the model specification.
    /// </note>
    /// </notes>
    [HttpPost]
    public async Task<ActionResult<NewFlightDTO>> PostFlight(CreateFlightCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            command.NewFlightDTO.UserId = userId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFlight = await _mediator.Send(command, cancellationToken);

            return Ok(createdFlight);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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