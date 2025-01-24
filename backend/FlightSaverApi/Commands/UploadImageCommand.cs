using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Commands;

public class UploadImageCommand : IRequest<string>
{
    public IFormFile Image { get; }

    public UploadImageCommand(IFormFile image)
    {
        Image = image ?? throw new ArgumentNullException(nameof(image));
    }
}