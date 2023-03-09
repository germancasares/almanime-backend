using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class SubtitleQueries
{
  public static Subtitle? GetByFansubIDAndEpisodeID(
    this DbSet<Subtitle> subtitles,
    Guid fansubID,
    Guid episodeID
  ) => subtitles.SingleOrDefault(subtitle => subtitle.Membership.FansubID == fansubID && subtitle.EpisodeID == episodeID);

  public static IQueryable<Subtitle> GetByAnimeSlugAndByPublished(
    this DbSet<Subtitle> subtitles,
    string animeSlug
  ) => subtitles.Where(subtitle => subtitle.Episode.Anime.Slug == animeSlug && subtitle.Status == ESubtitleStatus.Published);
}