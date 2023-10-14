namespace Almanime.Services.Interfaces;

public interface IFileService
{
  Task<(Stream Content, string ContentType)> Download(string blobName);
  Task<Uri> Upload(IFormFile file, string blobName);
  Task<Uri> Upload(IFormFile file, string blobName, IDictionary<string, string> metadata);
  Task Delete(string blobName);
}
