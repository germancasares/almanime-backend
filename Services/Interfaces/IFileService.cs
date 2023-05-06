using Almanime.Models.Enums;

namespace Almanime.Services.Interfaces;

public interface IFileService
{
  Task<(Stream Content, string ContentType, string)> DownloadSubtitle(string fansubAcronym, string animeSlug, int episodeNumber);
  Task<Uri> UploadSubtitle(IFormFile subtitle, string fansubAcronym, string animeSlug, int episodeNumber, string animeName, ESubtitleFormat format);
  Task DeleteSubtitle(string fansubAcronym, string animeSlug, int episodeNumber);
}
