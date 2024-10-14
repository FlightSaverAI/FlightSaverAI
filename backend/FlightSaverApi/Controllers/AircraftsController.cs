using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Models.Aircraft;
using FlightSaverApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace FlightSaverApi.Controllers
{
    [Route("/Aircrafts")]
    [ApiController]
    [Authorize]
    public class AircraftsController : ControllerBase
    {
        private readonly AircraftContext _context;

        public AircraftsController(AircraftContext context)
        {
            _context = context;
        }

        // GET: api/Aicrafts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftDTO>>> GetAircrafts()
        {
            return await _context.Aircrafts.Select(x => ItemToDTO(x)).ToListAsync();
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

            return ItemToDTO(plane);
        }

        // PUT: api/Aicrafts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAircraft(int id, AircraftDTO planeDTO)
        {
            if (id != planeDTO.Id)
            {
                return BadRequest();
            }

            var plane = await _context.Aircrafts.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }

            plane.Name = planeDTO.Name;
            plane.IataCode = planeDTO.IataCode;
            plane.IcaoCode = planeDTO.IcaoCode;
            plane.RegNumber = planeDTO.RegNumber;

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
        public async Task<ActionResult<AircraftDTO>> PostAircraft(AircraftDTO aircraftDTO)
        {
            var aircraft = new Aircraft
            {
                Name = aircraftDTO.Name,
                IataCode = aircraftDTO.IataCode,
                IcaoCode = aircraftDTO.IcaoCode,
                RegNumber = aircraftDTO.RegNumber,
            };

            _context.Aircrafts.Add(aircraft);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(PostAircraft),
                new { id = aircraft.Id },
                ItemToDTO(aircraft));
        }

        // DELETE: api/Aicrafts/5
        [HttpDelete("{id}")]
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

        private static AircraftDTO ItemToDTO(Aircraft aircraft) =>
            new AircraftDTO
            {
                Id = aircraft.Id,
                Name = aircraft.Name,
                IataCode = aircraft.IataCode,
                IcaoCode = aircraft.IcaoCode,
                RegNumber = aircraft.RegNumber
            };
    }
}
