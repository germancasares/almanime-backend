namespace Almanime.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID);
}
