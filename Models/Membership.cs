namespace Almanime.Models;

public class Membership : Base
{
    public Guid UserID { get; set; }
    public virtual User User { get; set; } = default!;


    public Guid RoleID { get; set; }
    public Guid FansubID { get; set; }
    public virtual FansubRole FansubRole { get; set; } = default!;


    public virtual ICollection<Subtitle> Subtitles { get; set; } = default!;
}
