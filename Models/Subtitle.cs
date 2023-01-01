using Almanime.Models.Enums;

namespace Almanime.Models;

public class Subtitle : Base
{
    public Subtitle(
        Guid id,
        ESubtitleStatus status,
        ESubtitleFormat format,
        string url,
        Guid episodeID,
        Guid membershipID
    ) : this(status, format, url, episodeID, membershipID)
    {
        ID = id;
    }

    protected Subtitle(
        ESubtitleStatus status,
        ESubtitleFormat format,
        string url,
        Guid episodeID,
        Guid membershipID
    )
    {
        Status = status;
        Format = format;
        Url = url ?? throw new ArgumentNullException(nameof(url));
        EpisodeID = episodeID;
        MembershipID = membershipID;
    }

    public ESubtitleStatus Status { get; set; }
    public ESubtitleFormat Format { get; set; }
    public string Url { get; set; }


    public Guid EpisodeID { get; set; }
    public virtual Episode Episode { get; set; } = default!;

    public Guid MembershipID { get; set; }
    public virtual Membership Membership { get; set; } = default!;
}
