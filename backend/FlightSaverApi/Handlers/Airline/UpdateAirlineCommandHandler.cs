using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.Commands.Airline;
using FlightSaverApi.Models.AirlineModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airline
{
    public class UpdateAirlineCommandHandler : IRequestHandler<UpdateAirlineCommand, AirlineDTO>
    {
        private readonly FlightSaverContext _context;
        private readonly IMapper _mapper;

        public UpdateAirlineCommandHandler(FlightSaverContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AirlineDTO> Handle(UpdateAirlineCommand request, CancellationToken cancellationToken)
        {
            var airline = await _context.Airlines
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (airline == null)
            {
                throw new KeyNotFoundException($"Airline with Id {request.Id} does not exist.");
            }

            _mapper.Map(request.AirlineDto, airline);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AirlineDTO>(airline);
        }
    }
}