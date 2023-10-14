using Almanime.Services.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using static Azure.Storage.Blobs.BlobClientOptions;

namespace Almanime.Services;

public class FileService : IFileService
{
  public const string BlobContainerName = "almanime";

  private readonly BlobServiceClient _blobServiceClient;

  public FileService(IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("AzureWebJobsStorage") ?? throw new InvalidOperationException("AzureWebJobsStorage connection string not set");
    _blobServiceClient = new BlobServiceClient(connectionString, new BlobClientOptions(ServiceVersion.V2021_12_02));
  }

  public async Task<(Stream Content, string ContentType)> Download(string blobName)
  {
    var blob = (await _blobServiceClient.GetBlobContainerClient(BlobContainerName).GetBlobClient(blobName).DownloadStreamingAsync()).Value;

    return (blob.Content, blob.Details.ContentType);
  }

  public async Task<Uri> Upload(IFormFile file, string blobName) => await Upload(file, blobName, new Dictionary<string, string>());
  public async Task<Uri> Upload(IFormFile file, string blobName, IDictionary<string, string> metadata)
  {
    var container = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
    await container.CreateIfNotExistsAsync();
    var blobClient = container.GetBlobClient(blobName);

    await blobClient.UploadAsync(file.OpenReadStream(), new BlobUploadOptions {
      Metadata = metadata,
    });

    return blobClient.Uri;
  }

  public async Task Delete(string blobName) => await _blobServiceClient.GetBlobContainerClient(BlobContainerName).DeleteBlobIfExistsAsync(blobName);
}
