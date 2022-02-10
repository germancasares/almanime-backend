using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface IEpisodeService
{
    IQueryable<Episode> GetByAnimeSlug(string animeSlug);
    //Dictionary<int, Dictionary<string, string>> GetFansubs(string animeSlug);
    Task Populate();
}
