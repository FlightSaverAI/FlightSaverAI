using Azure.Storage.Blobs;
using FlightSaverApi.Interfaces.Services;

namespace FlightSaverApi.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly string _connectionString;
    private readonly string _containerName;
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(IConfiguration configuration)
    {
        _connectionString = Environment.GetEnvironmentVariable("AzureBlobStorage_ConnectionString");
        _containerName = Environment.GetEnvironmentVariable("AzureBlobStorage_ContainerName");
        _containerClient = new BlobContainerClient(_connectionString, _containerName);
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        if (!allowedTypes.Contains(image.ContentType))
        {
            throw new InvalidOperationException("Invalid file type.");
        }

        // if (image.Length > 5 * 1024 * 1024)
        // {
        //     throw new InvalidOperationException("File size exceeds the 5 MB limit.");
        // }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var blobClient = _containerClient.GetBlobClient(fileName);

        using (var stream = image.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        return blobClient.Uri.AbsoluteUri;
    }
}