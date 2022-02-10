namespace Almanime.Models;

public class FansubRole : Base
{
    public string Name { get; set; }

    public Guid FansubID { get; set; }
    public virtual Fansub Fansub { get; set; } = default!;

    public virtual ICollection<Membership> Memberships { get; set; } = default!;
    public virtual ICollection<Permission> Permissions { get; set; } = default!;
}
