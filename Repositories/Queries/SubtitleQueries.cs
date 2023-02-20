using Almanime.Models;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class SubtitleQueries
{
  public static Subtitle? GetByFansubIDAndEpisodeID(
    this DbSet<Subtitle> subtitles,
    Guid fansubID,
    Guid episodeID
  ) => subtitles.SingleOrDefault(subtitle => subtitle.Membership.FansubID == fansubID && subtitle.EpisodeID == episodeID);
}