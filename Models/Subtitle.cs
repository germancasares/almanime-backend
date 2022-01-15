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
        Guid memberID
    ) : this(status, format, url, episodeID, memberID)
    {
        ID = id;
    }

    protected Subtitle(
        ESubtitleStatus status,
        ESubtitleFormat format,
        string url,
        Guid episodeID,
        Guid memberID
    )
    {
        Status = status;
        Format = format;
        Url = url ?? throw new ArgumentNullException(nameof(url));
        EpisodeID = episodeID;
        MemberID = memberID;
    }

    public ESubtitleStatus Status { get; set; }
    public ESubtitleFormat Format { get; set; }
    public string Url { get; set; }


    public Guid EpisodeID { get; set; }
    public virtual Episode Episode { get; set; } = default!;

    public Guid MemberID { get; set; }
    public virtual Member Member { get; set; } = default!;
}
