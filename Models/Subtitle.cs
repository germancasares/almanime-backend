using Almanime.Models.Enums;

namespace Almanime.Models;

public class Subtitle(
  ESubtitleStatus status,
  ESubtitleFormat format,
  ESubtitleLanguage language,
  string url,
  string blobName,
  Guid episodeID,
  Guid membershipID
    ) : Base
{
    public Subtitle(
      Guid id,
      ESubtitleStatus status,
      ESubtitleFormat format,
      ESubtitleLanguage language,
      string url,
      string blobName,
      Guid episodeID,
      Guid membershipID
    ) : this(status, format, language, url, blobName, episodeID, membershipID)
    {
        ID = id;
    }

    public ESubtitleStatus Status { get; set; } = status;
    public ESubtitleFormat Format { get; set; } = format;
    public ESubtitleLanguage Language { get; set; } = language;
    public string Url { get; set; } = url ?? throw new ArgumentNullException(nameof(url));
    public string BlobName { get; set; } = blobName ?? throw new ArgumentNullException(nameof(blobName));


    public Guid EpisodeID { get; set; } = episodeID;
    public virtual Episode Episode { get; set; } = default!;

    public Guid MembershipID { get; set; } = membershipID;
    public virtual Membership Membership { get; set; } = default!;
}
