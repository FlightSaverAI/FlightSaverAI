using FlightSaverApi.Commands.Aircraft;
using Microsoft.AspNetCore.Mvc;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Aircraft;
using Microsoft.AspNetCore.Authorization;
using FlightSaverApi.Queries.Aircraft;
using MediatR;

namespace FlightSaverApi.Controllers
{
    [Route("/aircraft")]
    [ApiController]
    [Authorize]
    public class AircraftController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AircraftController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET: /aircraft
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftDTO>>> GetAircrafts(CancellationToken cancellationToken)
        {
            var query = new GetAircraftsQuery();
            var aircrafts = await _mediator.Send(query, cancellationToken);
            
            return Ok(aircrafts);
        }
        
        /// <summary>
        /// Retrieves a list of minimal aircraft details.
        /// </summary>
        /// <remarks>
        /// This endpoint fetches basic details of aircraft, such as their ID and name, 
        /// for use in scenarios where only minimal data is required (e.g., dropdowns or summaries).
        /// The method uses a query handler to retrieve the data.
        /// </remarks>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        /// A list of minimal aircraft details, or an appropriate HTTP response if an error occurs.
        /// </returns>
        /// <response code="200">Returns the list of minimal aircraft details.</response>
        /// <response code="401">If the user is not authorized to access this resource.</response>
        /// <response code="500">If an internal server error occurs while processing the request.</response>
        [HttpGet("minimal")]
        public async Task<ActionResult<IEnumerable<MinimalAircraftDTO>>> GetMinimalAircrafts(CancellationToken cancellationToken)
        {
            var query = new GetMinimalAircraftsQuery();
            var aircrafts = await _mediator.Send(query, cancellationToken);
            
            return Ok(aircrafts);
        }
        
        // GET: /aircraft/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AircraftDTO>> GetAircraft([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var query = new GetAircraftQuery(id);
            var aircraft = await _mediator.Send(query, cancellationToken);

            return Ok(aircraft);
        }
        
        // PUT: /aircraft/{id}
        [HttpPut("{id:int}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> PutAircraft(int id, UpdateAircraftCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            
            await _mediator.Send(command, cancellationToken);
            
            return NoContent();
        }
        
        // POST: /aircraft
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<AircraftDTO>> PostAircraft(CreateAircraftCommand command,
            CancellationToken cancellationToken)
        {
            var createdAicraft = await _mediator.Send(command, cancellationToken);
            
            return CreatedAtAction(nameof(GetAircraft), new { id = createdAicraft.Id }, createdAicraft);
        }
        
        // DELETE: /aircraft/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteAircraft(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteAircraftCommand { Id = id };
            
            await _mediator.Send(command, cancellationToken);
            
            return NoContent();
        }
    }
}
