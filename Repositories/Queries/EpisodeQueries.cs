using Almanime.Models;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class EpisodeQueries
{
    public static Episode? GetByKitsuIdAndNumber(this DbSet<Episode> episodes, int kitsuId, int number) => episodes.SingleOrDefault(episode => episode.Anime.KitsuID == kitsuId && episode.Number == number);
    public static IQueryable<Episode> GetByAnimeSlug(this DbSet<Episode> episodes, string animeSlug) => episodes.Where(episode => episode.Anime.Slug == animeSlug);

}
