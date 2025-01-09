using FlightSaverApi.Commands.Aircraft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
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
        
        // GET: /aircraft/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AircraftDTO>> GetAircraft([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var query = new GetAircraftQuery(id);
            var aircraft = await _mediator.Send(query, cancellationToken);

            if (aircraft == null)
            {
                return NotFound();
            }
            
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
