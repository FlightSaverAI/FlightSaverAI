using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Data;
using Microsoft.AspNetCore.Authorization;
using FlightSaverApi.Models.AircraftModel;

namespace FlightSaverApi.Controllers
{
    [Route("/Aircrafts")]
    [ApiController]
    [Authorize]
    public class AircraftsController : ControllerBase
    {
        private readonly FlightSaverContext _context;

        public AircraftsController(FlightSaverContext context)
        {
            _context = context;
        }

        // GET: api/Aicrafts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftDTO>>> GetAircrafts()
        {
            return await _context.Aircrafts.Select(x => ItemToDto(x)).ToListAsync();
        }

        // GET: api/Aicrafts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftDTO>> GetAircraft(int id)
        {
            var plane = await _context.Aircrafts.FindAsync(id);

            if (plane == null)
            {
                return NotFound();
            }

            return ItemToDto(plane);
        }

        // PUT: api/Aicrafts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> PutAircraft(int id, AircraftDTO planeDto)
        {
            if (id != planeDto.Id)
            {
                return BadRequest();
            }

            var plane = await _context.Aircrafts.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }

            plane.Name = planeDto.Name;
            plane.IataCode = planeDto.IataCode;
            plane.IcaoCode = planeDto.IcaoCode;
            plane.RegNumber = planeDto.RegNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AircraftExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Aicrafts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<AircraftDTO>> PostAircraft(AircraftDTO aircraftDto)
        {
            var aircraft = new Aircraft
            {
                Name = aircraftDto.Name,
                IataCode = aircraftDto.IataCode,
                IcaoCode = aircraftDto.IcaoCode,
                RegNumber = aircraftDto.RegNumber,
                AirlineId = aircraftDto.AirlineId
            };

            _context.Aircrafts.Add(aircraft);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(PostAircraft),
                new { id = aircraft.Id },
                ItemToDto(aircraft));
        }

        // DELETE: api/Aicrafts/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteAircraft(int id)
        {
            var plane = await _context.Aircrafts.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }

            _context.Aircrafts.Remove(plane);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.Id == id);
        }

        private static AircraftDTO ItemToDto(Aircraft aircraft) =>
            new AircraftDTO
            {
                Id = aircraft.Id,
                Name = aircraft.Name,
                IataCode = aircraft.IataCode,
                IcaoCode = aircraft.IcaoCode,
                RegNumber = aircraft.RegNumber,
                AirlineId = aircraft.AirlineId
            };
    }
}
