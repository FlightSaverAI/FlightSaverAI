using AutoMapper;
using FlightSaverApi.DTOs;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<ImageUploadDTO, ImageRecord>()
            .ReverseMap();
    }
}