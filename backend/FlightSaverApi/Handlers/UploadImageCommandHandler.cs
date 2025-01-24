using FlightSaverApi.Commands;
using FlightSaverApi.Data;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Handlers;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, string>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly FlightSaverContext _dbContext;

    public UploadImageCommandHandler(IBlobStorageService blobStorageService, FlightSaverContext dbContext)
    {
        _blobStorageService = blobStorageService;
        _dbContext = dbContext;
    }

    public async Task<string> Handle(UploadImageCommand command, CancellationToken cancellationToken)
    {
        if (command.Image == null)
            throw new ArgumentException("No file uploaded.", nameof(command.Image));

        // Upload the image to Azure Blob Storage
        var imageUrl = await _blobStorageService.UploadImageAsync(command.Image);

        // Save the record to the database
        var imageRecord = new ImageRecord
        {
            Url = imageUrl
        };

        _dbContext.Images.Add(imageRecord);
        await _dbContext.SaveChangesAsync();

        return imageUrl;
    }
}