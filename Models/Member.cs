using Domain.Enums;

namespace Almanime.Models;

public class Member : Base
{
    public Guid FansubID { get; set; }
    public virtual Fansub Fansub { get; set; } = default!;
    public Guid UserID { get; set; }
    public virtual User User { get; set; } = default!;


    public EFansubRole Role { get; set; }

    public virtual ICollection<Subtitle> Subtitles { get; set; } = default!;
}
