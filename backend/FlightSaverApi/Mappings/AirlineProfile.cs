using AutoMapper;
using FlightSaverApi.DTOs.Airline;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class AirlineProfile : Profile
{
    public AirlineProfile()
    {
        CreateMap<AirlineDTO, Airline>()
            .ReverseMap();
            
        CreateMap<MinimalAirlineDTO, Airline>()
            .ReverseMap();
    }
}