using Almanime.Models.Enums;

namespace Almanime.Models;

public class Subtitle : Base
{
  public Subtitle(
    Guid id,
    ESubtitleStatus status,
    ESubtitleFormat format,
    ESubtitleLanguage language,
    string url,
    Guid episodeID,
    Guid membershipID
  ) : this(status, format, language, url, episodeID, membershipID)
  {
    ID = id;
  }

  public Subtitle(
    ESubtitleStatus status,
    ESubtitleFormat format,
    ESubtitleLanguage language,
    string url,
    Guid episodeID,
    Guid membershipID
  )
  {
    Status = status;
    Format = format;
    Language = language;
    Url = url ?? throw new ArgumentNullException(nameof(url));
    EpisodeID = episodeID;
    MembershipID = membershipID;
  }

  public ESubtitleStatus Status { get; set; }
  public ESubtitleFormat Format { get; set; }
  public ESubtitleLanguage Language { get; set; }
  public string Url { get; set; }


  public Guid EpisodeID { get; set; }
  public virtual Episode Episode { get; set; } = default!;

  public Guid MembershipID { get; set; }
  public virtual Membership Membership { get; set; } = default!;
}
