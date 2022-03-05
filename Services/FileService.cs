using Almanime.Services.Interfaces;
using Almanime.Utils;
using Azure.Storage.Blobs;

namespace Almanime.Services;

public class FileService : IFileService
{
    public const string BlobContainerName = "almanime";

    private readonly BlobServiceClient _blobServiceClient;

    public FileService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AzureWebJobsStorage");

        if (connectionString == null)
            throw new InvalidOperationException("AzureWebJobsStorage connection string not set"); // TODO

        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    private async Task<string> Upload(string reference, string blob, IFormFile file)
    {
        var container = _blobServiceClient.GetBlobContainerClient(reference);

        await container.CreateIfNotExistsAsync();

        var blobClient = container.GetBlobClient(blob);
        await blobClient.UploadAsync(file.OpenReadStream());

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID) => 
        await Upload(BlobContainerName, $"fansub/{fansubID}/subtitles/{subtitleID}{subtitle.GetExtension()}", subtitle);
}
