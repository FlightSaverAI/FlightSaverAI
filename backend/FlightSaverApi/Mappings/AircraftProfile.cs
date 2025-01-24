using AutoMapper;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class AircraftProfile : Profile
{
    public AircraftProfile()
    {
        CreateMap<AircraftDTO, Aircraft>()
            .ReverseMap();
            
        CreateMap<MinimalAircraftDTO, Aircraft>()
            .ReverseMap();
    }
}