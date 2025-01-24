using AutoMapper;
using FlightSaverApi.DTOs.Airport;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<AirportDTO, Airport>()
            .ReverseMap();
            
        CreateMap<MinimalAirportDTO, Airport>()
            .ReverseMap();
    }
}