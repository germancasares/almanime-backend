using Almanime.Models;
using Almanime.Models.Enums;

namespace Almanime.Services.Interfaces;

public interface ISubtitleService
{
    IQueryable<Subtitle> GetByAnimeSlug(string animeSlug);

    Subtitle Publish(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber);
    Subtitle Unpublish(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber);

    Task<(Stream Content, string ContentType, string)> GetFile(string fansubAcronym, string animeSlug, int episodeNumber);
    Task<Subtitle> CreateOrUpdate(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber, ESubtitleLanguage language, IFormFile file);
    Task Delete(string auth0ID, string fansubAcronym, string animeSlug, int episodeNumber);
}
