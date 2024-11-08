using FlightSaverApi.Commands.Aircraft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Data;
using Microsoft.AspNetCore.Authorization;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Queries.Aircraft;
using MediatR;

namespace FlightSaverApi.Controllers
{
    [Route("/Aircrafts")]
    [ApiController]
    [Authorize]
    public class AircraftsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AircraftsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET: /Aircrafts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aircraft>>> GetAircrafts(CancellationToken cancellationToken)
        {
            var query = new GetAircraftsQuery();
            var aircrafts = await _mediator.Send(query, cancellationToken);
            
            return Ok(aircrafts);
        }
        
        // GET: /Aircrafts/{id}
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
        
        // PUT: /Aircrafts/{id}
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
        
        // POST: /Aircrafts
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<AircraftDTO>> PostAircraft(CreateAircraftCommand command,
            CancellationToken cancellationToken)
        {
            var createdAicraft = await _mediator.Send(command, cancellationToken);
            
            return CreatedAtAction(nameof(GetAircraft), new { id = createdAicraft.Id }, createdAicraft);
        }
        
        // DELETE: /Aircrafts/{id}
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
