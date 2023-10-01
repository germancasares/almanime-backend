using Almanime.Models.Enums;
using Almanime.Services.Interfaces;
using Azure;
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

  private async Task<Uri> Upload(IFormFile file, string blob, IDictionary<string, string> metadata)
  {
    var container = _blobServiceClient.GetBlobContainerClient(BlobContainerName);

    await container.CreateIfNotExistsAsync();

    var blobClient = container.GetBlobClient(blob);
    await blobClient.UploadAsync(file.OpenReadStream(), new BlobUploadOptions
    {
      Metadata = metadata,
    });

    return blobClient.Uri;
  }

  private async Task<Response<BlobDownloadStreamingResult>> Download(string blob) => await _blobServiceClient.GetBlobContainerClient(BlobContainerName).GetBlobClient(blob).DownloadStreamingAsync();

  private async Task Delete(string blob) => await _blobServiceClient.GetBlobContainerClient(BlobContainerName).DeleteBlobIfExistsAsync(blob);

  public async Task<Uri> UploadSubtitle(
    IFormFile subtitle,
    string fansubAcronym,
    string animeSlug,
    int episodeNumber,
    string animeName,
    ESubtitleFormat format
  )
  {
    var blobName = $"fansub/{fansubAcronym}/{animeSlug}/{episodeNumber}";

    var metadata = new Dictionary<string, string>
    {
      { "AnimeName", Uri.EscapeDataString(animeName) },
      { "ESubtitleFormat", Uri.EscapeDataString(format.ToString()) },
    };

    return await Upload(subtitle, blobName, metadata);
  }

  public async Task<(Stream Content, string ContentType, string)> DownloadSubtitle(string fansubAcronym, string animeSlug, int episodeNumber)
  {
    var blob = (await Download($"fansub/{fansubAcronym}/{animeSlug}/{episodeNumber}")).Value;

    var animeName = blob.Details.Metadata.SingleOrDefault(metadata => metadata.Key == "AnimeName");
    var subtitleFormat = blob.Details.Metadata.SingleOrDefault(metadata => metadata.Key == "ESubtitleFormat");

    return (blob.Content, blob.Details.ContentType, $"[{fansubAcronym}]{animeName.Value} - Episode {episodeNumber}.{subtitleFormat.Value.ToLower()}");
  }

  public async Task DeleteSubtitle(string fansubAcronym, string animeSlug, int episodeNumber) => await Delete($"fansub/{fansubAcronym}/{animeSlug}/{episodeNumber}");
}
